using Chilindo_Database.ViewModel;
using ChilinDo_Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Chilindo_Banking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly ITransactionService _transactionService;

        public AccountController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet("balance/{id}")]
        public async Task<ActionResult> GetBalance(int accountNumber)
        {
            return Json(new TransactionBaseResponse());
        }

        [HttpPost("deposit")]
        public async Task<ActionResult> Deposit([FromBody] string value)
        {
            return Json(Ok());
        }

        [HttpPost("withdraw")]
        public async Task<ActionResult> Withdraw([FromBody] string value)
        {
            return Json(Ok());
        }
    }
}