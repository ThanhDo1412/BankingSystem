using BankingService.ViewModel;
using Newtonsoft.Json;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BankingTest.ConcurencyTest
{
    [TestFixture]
    public class TransactionConcurencyTest : BaseTest
    {
        [Test]
        public async Task DepositExistedCurrency()
        {
            //Arr
            var transaction = new TransactionBaseRequest(1, 200, "USD");
            var deposteTasks = new List<Task<TransactionBaseResponse>>();
            var withdrawTasks = new List<Task<TransactionBaseResponse>>();
            decimal balance = 0;

            //Act
            for (int i = 0; i < 3; i++)
            {
                deposteTasks.Add(_transactionService.Deposit(transaction));
                withdrawTasks.Add(_transactionService.Withdraw(transaction));
            }

            var collection1 = await Task.WhenAll(deposteTasks);
            var collection2 = await Task.WhenAll(withdrawTasks);

            balance = collection1.Max(x => x.Balance);

            ////Assert
            //task.Result.ShouldBeOfType<TransactionBaseResponse>();
            //task.Result.AccountNumber.ShouldBe(1);
            //task.Result.Balance.ShouldBe(1200);
            //task.Result.Currency.ShouldBe("USD");
            //task.Result.Successful.ShouldBe(true);
            //task.Result.Message.ShouldBe("Transaction succeeded");
        }




        [Test]
        public void WithdrawExistedCurrency()
        {
            //Arr
            var transaction = new TransactionBaseRequest(1, 200, "USD");
            var requestUrl = @"http://localhost:5026/api/deposit";

            //Act
            var task = PostAsyn(requestUrl, transaction);
            task.Wait();

            //Assert
            task.Result.ShouldBeOfType<TransactionBaseResponse>();
            task.Result.AccountNumber.ShouldBe(1);
            task.Result.Balance.ShouldBe(800);
            task.Result.Currency.ShouldBe("USD");
            task.Result.Successful.ShouldBe(true);
            task.Result.Message.ShouldBe("Transaction succeeded");
        }

        private async Task<TransactionBaseResponse> PostAsyn(string requestUrl, TransactionBaseRequest requestModel)
        {
            HttpContent content = new StringContent(JsonConvert.SerializeObject(requestModel), Encoding.UTF8, "application/json");
            var client = new HttpClient()
            {
                BaseAddress = new Uri(requestUrl)
            };
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.PostAsync(requestUrl, content);

            return JsonConvert.DeserializeObject<TransactionBaseResponse>(await response.Content.ReadAsStringAsync());
        }
    }
}
