using BankingData.Data;
using BankingData.Helper;

namespace BankingService.ViewModel
{
    public class TransactionBaseRequest
    {
        public TransactionBaseRequest(int accountNumber, decimal amount, string currency)
        {
            AccountNumber = accountNumber;
            Amount = amount;
            Currency = currency;
        }

        public int AccountNumber { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }

        public void Validate()
        {
            if (AccountNumber <= 0)
            {
                throw new CustomException(ErrorCode.E5, AccountNumber);
            }
            else if (Amount <= 0)
            {
                throw new CustomException(ErrorCode.E6, AccountNumber);
            }
        }
    }
}
