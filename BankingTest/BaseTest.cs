using BankingData;
using BankingData.UnitOfWork;
using BankingService;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankingTest
{
    [TestFixture]
    public class BaseTest
    {
        protected ChilinDoContext _fakeDbContext;
        protected UnitOfWork _fakeuow;
        protected TransactionService _transactionService;

        [SetUp]
        public void SetUpEnv()
        {
            var options = new DbContextOptionsBuilder<ChilinDoContext>()
                .UseInMemoryDatabase(databaseName: "BankingTest")
                .Options;
            _fakeDbContext = new ChilinDoContext(options);
            _fakeuow = new UnitOfWork(_fakeDbContext);
            FakeDB.InitDatabase(_fakeDbContext);
            _transactionService = new TransactionService(_fakeuow);
        }

        [TearDown]
        public void ResetEnv()
        {
            _fakeDbContext.Database.EnsureDeleted();
        }
    }
}
