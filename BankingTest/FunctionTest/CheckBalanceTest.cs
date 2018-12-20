using BankingData.Helper;
using BankingService.ViewModel;
using NUnit.Framework;
using Shouldly;
using System.Threading.Tasks;

namespace BankingTest.FunctionTest
{
    [TestFixture]
    public class CheckBalanceTest : BaseTest
    {
        [Test]
        public async Task WorkedCase()
        {
            //Arr

            //Act
            var result = await _transactionService.GetBalance(1);

            //Assert
            result.Count.ShouldBe(2);
            foreach (var transactionBaseResponse in result)
            {
                transactionBaseResponse.ShouldBeOfType<TransactionBaseResponse>();
            }
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
        public void InputInvalidAccountNumber()
        {
            //Arr

            //Act
            var result = Assert.ThrowsAsync<CustomException>(() => _transactionService.GetBalance(-1));

            //Assert
            result.ShouldBeOfType<CustomException>();
            result.ErrorMessage.ShouldBe("Account Number can not be negative");
            result.ErrorCode.ShouldBe("E5");
            result.AccountNumber.ShouldBe(-1);
        }
    }
}
