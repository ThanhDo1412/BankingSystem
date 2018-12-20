using System.Linq;
using BankingData.Helper;
using BankingService.ViewModel;
using NUnit.Framework;
using Shouldly;
using System.Threading.Tasks;

namespace BankingTest.FunctionTest
{
    [TestFixture]
    public class DepositTest : BaseTest
    {
        [Test]
        public async Task DepositExistedCurrency()
        {
            //Arr
            var transaction = new TransactionBaseRequest(1, 200, "USD");

            //Act
            var result = await _transactionService.Deposit(transaction);

            //Assert
            result.ShouldBeOfType<TransactionBaseResponse>();
            result.AccountNumber.ShouldBe(1);
            result.Balance.ShouldBe(1200);
            result.Currency.ShouldBe("USD");
            result.Successful.ShouldBe(true);
            result.Message.ShouldBe("Transaction succeeded");

            var trasactionHistory = (await _fakeuow.TransactionHistoryRepo.FindAllAsync()).OrderByDescending(x => x.Id).FirstOrDefault();

            trasactionHistory.ShouldNotBeNull();
            trasactionHistory.AccountId.ShouldBe(1);
            trasactionHistory.Amount.ShouldBe(200);
            trasactionHistory.Currency.ShouldBe("USD");
            trasactionHistory.IsSuccess.ShouldBe(true);
        }

        [Test]
        public async Task DepositNewCurrency()
        {
            //Arr
            var transaction = new TransactionBaseRequest(1, 500000, "VND");

            //Act
            var result = await _transactionService.Deposit(transaction);

            //Assert
            result.ShouldBeOfType<TransactionBaseResponse>();
            result.AccountNumber.ShouldBe(1);
            result.Balance.ShouldBe(500000);
            result.Currency.ShouldBe("VND");
            result.Successful.ShouldBe(true);
            result.Message.ShouldBe("Transaction succeeded");

            var trasactionHistory = (await _fakeuow.TransactionHistoryRepo.FindAllAsync()).OrderByDescending(x => x.Id).FirstOrDefault();

            trasactionHistory.AccountId.ShouldBe(1);
            trasactionHistory.Amount.ShouldBe(500000);
            trasactionHistory.Currency.ShouldBe("VND");
            trasactionHistory.IsSuccess.ShouldBe(true);
        }

        [Test]
        public void InsertNotExistedAccountNumber()
        {
            //Arr
            var transaction = new TransactionBaseRequest(5, 500000, "VND");

            //Act
            var result = Assert.ThrowsAsync<CustomException>(() => _transactionService.Deposit(transaction));

            //Assert
            result.ShouldBeOfType<CustomException>();
            result.AccountNumber.ShouldBe(5);
            result.ErrorCode.ShouldBe("E3");
            result.ErrorMessage.ShouldBe("Account number does not existed");
        }

        [Test]
        public void InsertInValidAccountNumber()
        {
            //Arr
            var transaction = new TransactionBaseRequest(-4, 500000, "VND");

            //Act
            var result = Assert.ThrowsAsync<CustomException>(() => _transactionService.Deposit(transaction));

            //Assert
            result.ShouldBeOfType<CustomException>();
            result.ErrorMessage.ShouldBe("Account Number can not be negative");
            result.ErrorCode.ShouldBe("E5");
            result.AccountNumber.ShouldBe(-4);
        }

        [Test]
        public void InsertInValidAmount()
        {
            //Arr
            var transaction = new TransactionBaseRequest(1, -500000, "VND");

            //Act
            var result = Assert.ThrowsAsync<CustomException>(() => _transactionService.Deposit(transaction));

            //Assert
            result.ShouldBeOfType<CustomException>();
            result.ErrorMessage.ShouldBe("Amount can not be negative");
            result.ErrorCode.ShouldBe("E6");
            result.AccountNumber.ShouldBe(1);
        }

        [Test]
        public void InsertInValidCurrency()
        {
            //Arr
            var transaction = new TransactionBaseRequest(1, 500000, null);

            //Act
            var result = Assert.ThrowsAsync<CustomException>(() => _transactionService.Deposit(transaction));

            //Assert
            result.ShouldBeOfType<CustomException>();
            result.ErrorMessage.ShouldBe("Currency can not be null or empty");
            result.ErrorCode.ShouldBe("E7");
            result.AccountNumber.ShouldBe(1);
        }

        [Test]
        public void InsertInValidAccountNumberAndAmount()
        {
            //Arr
            var transaction = new TransactionBaseRequest(-4, -500000, "VND");

            //Act
            var result = Assert.ThrowsAsync<CustomException>(() => _transactionService.Deposit(transaction));

            //Assert
            result.ShouldBeOfType<CustomException>();
            result.ErrorMessage.ShouldBe("Account Number can not be negative");
            result.ErrorCode.ShouldBe("E5");
            result.AccountNumber.ShouldBe(-4);
        }

        [Test]
        public void InsertInValidAccountNumberAndCurrency()
        {
            //Arr
            var transaction = new TransactionBaseRequest(-4, 500000, null);

            //Act
            var result = Assert.ThrowsAsync<CustomException>(() => _transactionService.Deposit(transaction));

            //Assert
            result.ShouldBeOfType<CustomException>();
            result.ErrorMessage.ShouldBe("Account Number can not be negative");
            result.ErrorCode.ShouldBe("E5");
            result.AccountNumber.ShouldBe(-4);
        }

        [Test]
        public void InsertInValidAmountAndCurrency()
        {
            //Arr
            var transaction = new TransactionBaseRequest(1, -500000, null);

            //Act
            var result = Assert.ThrowsAsync<CustomException>(() => _transactionService.Deposit(transaction));

            //Assert
            result.ShouldBeOfType<CustomException>();
            result.ErrorMessage.ShouldBe("Amount can not be negative");
            result.ErrorCode.ShouldBe("E6");
            result.AccountNumber.ShouldBe(1);
        }

        [Test]
        public void InsertInValidAccountNumberAndAmountAndCurrency()
        {
            //Arr
            var transaction = new TransactionBaseRequest(-4, -500000, null);

            //Act
            var result = Assert.ThrowsAsync<CustomException>(() => _transactionService.Deposit(transaction));

            //Assert
            result.ShouldBeOfType<CustomException>();
            result.ErrorMessage.ShouldBe("Account Number can not be negative");
            result.ErrorCode.ShouldBe("E5");
            result.AccountNumber.ShouldBe(-4);
        }
    }
}
