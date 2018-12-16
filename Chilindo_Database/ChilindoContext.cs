using System;
using Chilindo_Database.Entity;
using Microsoft.EntityFrameworkCore;

namespace Chilindo_Database
{
    public class ChilinDoContext : DbContext
    {
        public ChilinDoContext(DbContextOptions<ChilinDoContext> options)
            : base(options)
        {
        }

        public DbSet<AccountInfo> AccountInfos { get; set; }
        public DbSet<AccountDetail> AccountDetails { get; set; }
        public DbSet<TransactionHistory> TransactionHistories { get; set; }

        #region Create init data

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountInfo>().HasData(new[]
            {
                new AccountInfo { Id = 1, AccountName = "AccountA" },
                new AccountInfo { Id = 2, AccountName = "AccountB" },
            });

            modelBuilder.Entity<AccountDetail>().HasData(new[]
            {
                new AccountDetail { Id = 1, AcountInfoId = 1, Balance = 1000, Currency = "USD"},
                new AccountDetail { Id = 2, AcountInfoId = 1, Balance = 1000, Currency = "MYR"},
                new AccountDetail { Id = 3, AcountInfoId = 2, Balance = 1000000, Currency = "VND"},
                new AccountDetail { Id = 4, AcountInfoId = 2, Balance = 1000000, Currency = "BAHT"},
                new AccountDetail { Id = 5, AcountInfoId = 2, Balance = 3000, Currency = "USD"},
            });

            modelBuilder.Entity<TransactionHistory>().HasData(new[]
            {
                new TransactionHistory { Id = 1, AccountId = 1, Amount = 1000, Currency = "USD", IsSuccess = true},
                new TransactionHistory { Id = 2, AccountId = 1, Amount = 1000, Currency = "MYR", IsSuccess = true},
                new TransactionHistory { Id = 3, AccountId = 2, Amount = 1000000, Currency = "VND", IsSuccess = true},
                new TransactionHistory { Id = 4, AccountId = 2, Amount = 1000000, Currency = "BAHT", IsSuccess = true},
                new TransactionHistory { Id = 5, AccountId = 2, Amount = 3000, Currency = "USD", IsSuccess = true}
            });
        }

        #endregion
    }
}
