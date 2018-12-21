using BankingData;
using BankingData.Entity;

namespace BankingTest
{
    public class FakeDB
    {
        public static void InitDatabase(ChilinDoContext db)
        {
            db.AccountInfos.AddRange(new []
            {
                new AccountInfo { Id = 1, AccountName = "AccountA" },
                new AccountInfo { Id = 2, AccountName = "AccountB" },
            });

            db.AccountDetails.AddRange(new[]
            {
                new AccountDetail { AcountInfoId = 1, Balance = 1000, Currency = "USD"},
                new AccountDetail { AcountInfoId = 1, Balance = 1000, Currency = "MYR"},
                new AccountDetail { AcountInfoId = 2, Balance = 1000000, Currency = "VND"},
                new AccountDetail { AcountInfoId = 2, Balance = 1000000, Currency = "BAHT"},
                new AccountDetail { AcountInfoId = 2, Balance = 3000, Currency = "USD"},
            });

            db.TransactionHistories.AddRange(new[]
            {
                new TransactionHistory { AccountId = 1, Amount = 1000, Currency = "USD", IsSuccess = true},
                new TransactionHistory { AccountId = 1, Amount = 1000, Currency = "MYR", IsSuccess = true},
                new TransactionHistory { AccountId = 2, Amount = 1000000, Currency = "VND", IsSuccess = true},
                new TransactionHistory { AccountId = 2, Amount = 1000000, Currency = "BAHT", IsSuccess = true},
                new TransactionHistory { AccountId = 2, Amount = 3000, Currency = "USD", IsSuccess = true}
            });

            db.SaveChanges();
        }
    }
}
