using System;
using System.Collections.Generic;
using System.Text;
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
                new AccountDetail { Id = 1, AcountInfoId = 1, Balance = 1000, Currency = "USD"},
                new AccountDetail { Id = 2, AcountInfoId = 1, Balance = 1000, Currency = "MYR"},
                new AccountDetail { Id = 3, AcountInfoId = 2, Balance = 1000000, Currency = "VND"},
                new AccountDetail { Id = 4, AcountInfoId = 2, Balance = 1000000, Currency = "BAHT"},
                new AccountDetail { Id = 5, AcountInfoId = 2, Balance = 3000, Currency = "USD"},
            });

            db.TransactionHistories.AddRange(new[]
            {
                new TransactionHistory { Id = 1, AccountId = 1, Amount = 1000, Currency = "USD", IsSuccess = true},
                new TransactionHistory { Id = 2, AccountId = 1, Amount = 1000, Currency = "MYR", IsSuccess = true},
                new TransactionHistory { Id = 3, AccountId = 2, Amount = 1000000, Currency = "VND", IsSuccess = true},
                new TransactionHistory { Id = 4, AccountId = 2, Amount = 1000000, Currency = "BAHT", IsSuccess = true},
                new TransactionHistory { Id = 5, AccountId = 2, Amount = 3000, Currency = "USD", IsSuccess = true}
            });

            db.SaveChanges();
        }
    }
}
