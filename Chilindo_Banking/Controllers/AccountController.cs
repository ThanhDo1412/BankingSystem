using Chilindo_Database.ViewModel;
using ChilinDo_Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Chilindo_Banking.Controllers
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
            return Json(await _transactionService.GetBalance(accountNumber));
        }

        [HttpPost("deposit")]
        public async Task<ActionResult> Deposit([FromBody] TransactionBaseRequest request)
        {
            return Json(await _transactionService.Deposit(request));
        }

        [HttpPost("withdraw")]
        public async Task<ActionResult> Withdraw([FromBody] TransactionBaseRequest request)
        {
            return Json(await _transactionService.Withdraw(request));
        }
    }
}