using System;
using System.Collections.Generic;
using System.Text;
using BankingService.ViewModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BankingClient
{
    public class CheckBalanceResponse
    {
        [JsonConverter(typeof(SingleOrArrayConverter<string>))]
        public List<TransactionBaseResponse> items { get; set; }
    }

    class SingleOrArrayConverter<T> : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(List<T>));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JToken token = JToken.Load(reader);
            if (token.Type == JTokenType.Array)
            {
                return token.ToObject<List<T>>();
            }
            return new List<T> { token.ToObject<T>() };
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
