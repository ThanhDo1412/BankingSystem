using BankingData.Helper;
using BankingService.ViewModel;
using NUnit.Framework;
using Shouldly;
using System.Threading.Tasks;
using System.Linq;

namespace BankingTest.FunctionTest
{
    [TestFixture]
    public class WithDrawTest : BaseTest
    {
        [Test]
        public async Task WithdrawExistedCurrency()
        {
            //Arr
            var transaction = new TransactionBaseRequest(1, 200, "USD");

            //Act
            var result = await _transactionService.Withdraw(transaction);

            //Assert
            result.ShouldBeOfType<TransactionBaseResponse>();
            result.AccountNumber.ShouldBe(1);
            result.Balance.ShouldBe(800);
            result.Currency.ShouldBe("USD");
            result.Successful.ShouldBe(true);
            result.Message.ShouldBe("Transaction succeeded");

            var trasactionHistory = (await _fakeuow.TransactionHistoryRepo.FindAllAsync()).OrderByDescending(x => x.Id).FirstOrDefault();

            trasactionHistory.AccountId.ShouldBe(1);
            trasactionHistory.Amount.ShouldBe(200);
            trasactionHistory.Currency.ShouldBe("USD");
            trasactionHistory.IsSuccess.ShouldBe(true);
        }

        [Test]
        public void WithdrawNewCurrency()
        {
            //Arr
            var transaction = new TransactionBaseRequest(1, 500000, "VND");

            //Act
            var result = Assert.ThrowsAsync<CustomException>(() => _transactionService.Withdraw(transaction));

            //Assert
            result.ShouldBeOfType<CustomException>();
            result.AccountNumber.ShouldBe(1);
            result.ErrorCode.ShouldBe("E4");
            result.ErrorMessage.ShouldBe("This account doesn't have this currency. Please try with another currency");
        }

        [Test]
        public void WithdrawOutOfBanlance()
        {
            //Arr
            var transaction = new TransactionBaseRequest(1, 2000, "USD");

            //Act
            var result = Assert.ThrowsAsync<CustomException>(() => _transactionService.Withdraw(transaction));

            //Assert
            result.ShouldBeOfType<CustomException>();
            result.AccountNumber.ShouldBe(1);
            result.ErrorCode.ShouldBe("E1");
            result.ErrorMessage.ShouldBe("The Balance is not enough for withdrawal");
        }

        [Test]
        public void InsertNotExistedAccountNumber()
        {
            //Arr
            var transaction = new TransactionBaseRequest(5, 500000, "VND");

            //Act
            var result = Assert.ThrowsAsync<CustomException>(() => _transactionService.Withdraw(transaction));

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
            var result = Assert.ThrowsAsync<CustomException>(() => _transactionService.Withdraw(transaction));

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
            var result = Assert.ThrowsAsync<CustomException>(() => _transactionService.Withdraw(transaction));

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
            var result = Assert.ThrowsAsync<CustomException>(() => _transactionService.Withdraw(transaction));

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
            var result = Assert.ThrowsAsync<CustomException>(() => _transactionService.Withdraw(transaction));

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
            var result = Assert.ThrowsAsync<CustomException>(() => _transactionService.Withdraw(transaction));

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
            var result = Assert.ThrowsAsync<CustomException>(() => _transactionService.Withdraw(transaction));

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
            var result = Assert.ThrowsAsync<CustomException>(() => _transactionService.Withdraw(transaction));

            //Assert
            result.ShouldBeOfType<CustomException>();
            result.ErrorMessage.ShouldBe("Account Number can not be negative");
            result.ErrorCode.ShouldBe("E5");
            result.AccountNumber.ShouldBe(-4);
        }
    }
}
