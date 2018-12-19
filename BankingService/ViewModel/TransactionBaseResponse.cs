using Newtonsoft.Json;

namespace BankingService.ViewModel
{
    public class TransactionBaseResponse
    {
        [JsonProperty("accountNumber")]
        public int AccountNumber { get; set; }
        [JsonProperty("successful")]
        public bool Successful { get; set; }
        [JsonProperty("balance")]
        public decimal Balance { get; set; }
        [JsonProperty("currency")]
        public string Currency { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
