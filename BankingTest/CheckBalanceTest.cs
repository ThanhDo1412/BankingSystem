using System.Linq;
using System.Threading.Tasks;
using BankingData;
using BankingData.Entity;
using BankingData.Helper;
using BankingData.UnitOfWork;
using BankingService;
using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using FizzWare.NBuilder;
using Shouldly;

namespace BankingTest
{
    [TestFixture]
    public class CheckBalanceTest
    {
        private ChilinDoContext _fakeDbContext;
        private UnitOfWork _fakeuow;
        private TransactionService _transactionService;

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
        public void a()
        {
            _fakeDbContext.AccountDetails.RemoveRange(_fakeDbContext.AccountDetails.Select(x => x));
            _fakeDbContext.AccountInfos.RemoveRange();
            _fakeDbContext.TransactionHistories.RemoveRange();
            _fakeDbContext.SaveChanges();
        }

        [Test]
        public async Task WorkedCase()
        {
            //Arr

            //Act
            var result = await _transactionService.GetBalance(1);

            //Assert
            result.Count.ShouldBe(2);
        }

        [Test]
        public async Task AccountNotExisted()
        {
            //Arr

            //Act
            var result = await _transactionService.GetBalance(5);

            //Assert
            result.Count.ShouldBe(0);
        }

        [Test]
        public async Task InputInvalidAccountNumber()
        {
            //Arr

            //Act
            var result = Assert.ThrowsAsync<CustomException>(() => _transactionService.GetBalance(-1));

            //Assert
            result.ErrorMessage.ShouldBe("Account Number can not be negative");
            result.ErrorCode.ShouldBe("E5");
            result.AccountNumber.ShouldBe(-1);
        }
    }
}
