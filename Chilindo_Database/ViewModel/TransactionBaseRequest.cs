namespace Chilindo_Database.ViewModel
{
    public class TransactionBaseRequest
    {
        public int AccountNumber { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
    }
}
