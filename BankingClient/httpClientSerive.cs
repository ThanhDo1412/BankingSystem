using BankingService.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BankingClient
{
    public class httpClientSerive
    {
        private readonly string _apiUrl = @"http://localhost:5026/api/";
        private readonly JsonSerializer _jsonSerializer = new JsonSerializer();

        public async Task<string> GetBalance(int accountNumber)
        {
            return await GetAsync($"account/balance/{accountNumber}");
        }

        public async Task<TransactionBaseResponse> PostDeposit(TransactionBaseRequest request)
        {
            return await PostAsync<TransactionBaseRequest, TransactionBaseResponse>("account/deposit", request);
        }

        public async Task<TransactionBaseResponse> PostWithdraw(TransactionBaseRequest request)
        {
            return await PostAsync<TransactionBaseRequest, TransactionBaseResponse>("account/withdraw", request);
        }


        #region Private function

        private async Task<string> GetAsync(string requestUrl, HttpContent content = null)
        {
            if (content == null) content = new StringContent(string.Empty, Encoding.UTF8, "application/json");
            var response = await SendAsync(HttpMethod.Get, requestUrl, content);

            return await response.Content.ReadAsStringAsync();
        }

        private async Task<TResponse> PostAsync<TRequest, TResponse>(string requestUrl, TRequest request)
        {
            return await PostAsync<TResponse>(requestUrl, new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));
        }

        private async Task<TResponse> PostAsync<TResponse>(string requestUrl, HttpContent content = null)
        {
            if (content == null) content = new StringContent(string.Empty, Encoding.UTF8, "application/json");
            var response = await SendAsync(HttpMethod.Post, requestUrl, content);
            using (var stream = await response.Content.ReadAsStreamAsync())
            {
                using (var streamReader = new StreamReader(stream))
                {
                    using (var jsonReader = new JsonTextReader(streamReader))
                    {
                        if (response.StatusCode == HttpStatusCode.InternalServerError)
                        {
                            var objError = _jsonSerializer.Deserialize<IDictionary<string, string>>(jsonReader);
                            var message = objError.ContainsKey("error") ? objError["error"] : "Can't get data from API Core.";
                            Console.WriteLine(message);
                        }

                        return response.StatusCode == HttpStatusCode.OK ? _jsonSerializer.Deserialize<TResponse>(jsonReader) : default(TResponse);
                    }
                }
            }
        }

        private async Task<HttpResponseMessage> SendAsync(HttpMethod method, string requestUrl, HttpContent content)
        {
            HttpClient client = BuildHttpClient(_apiUrl);

            var request = new HttpRequestMessage
            {
                Method = method,
                RequestUri = new Uri(client.BaseAddress, requestUrl),
                Content = content
            };

            var response = await client.SendAsync(request);

            return response;
        }

        private HttpClient BuildHttpClient(string apiUrl)
        {
            var handler = new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };

            var httpClientInstance = new HttpClient(handler)
            {
                BaseAddress = new Uri(apiUrl),
                Timeout = TimeSpan.FromMinutes(5)
            };

            httpClientInstance.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return httpClientInstance;
        }

        #endregion
    }
}
