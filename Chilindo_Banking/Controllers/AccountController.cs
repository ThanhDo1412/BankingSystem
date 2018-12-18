using System;
using Chilindo_Database.ViewModel;
using ChilinDo_Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Chilindo_Data.Data;
using Chilindo_Data.Helper;
using Microsoft.AspNetCore.Http;

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