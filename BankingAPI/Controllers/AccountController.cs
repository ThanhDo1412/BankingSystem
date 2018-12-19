using BankingService.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BankingService.ViewModel;

namespace BankingApi.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly ITransactionService _transactionService;

        public AccountController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet("balance/{accountNumber}")]
        public async Task<ActionResult> GetBalance(int accountNumber)
        {
            HttpContext.Session.SetInt32("AccountNumber", accountNumber); 
            return Json(await _transactionService.GetBalance(accountNumber));
        }

        [HttpPost("deposit")]
        public async Task<ActionResult> Deposit([FromBody] TransactionBaseRequest request)
        {
            HttpContext.Session.SetInt32("AccountNumber", request.AccountNumber);
            return Json(await _transactionService.Deposit(request));
        }

        [HttpPost("withdraw")]
        public async Task<ActionResult> Withdraw([FromBody] TransactionBaseRequest request)
        {
            HttpContext.Session.SetInt32("AccountNumber", request.AccountNumber);
            return Json(await _transactionService.Withdraw(request));
        }
    }
}