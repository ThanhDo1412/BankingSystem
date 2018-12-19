using System;
using System.Data;
using System.Threading.Tasks;
using BankingData.Data;
using BankingData.Helper;
using BankingService.Interface;
using BankingService.ViewModel;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Serilog;

namespace BankingApi.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, ITransactionService transactionService)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(context, exception, transactionService);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception, ITransactionService transactionService)
        {
            var response = new TransactionBaseResponse
            {
                Successful = false
            };
            var accountNumber = 0;

            switch (exception)
            {
                //Custom error showing to user
                case CustomException e:
                    response.Message = e.ErrorMessage;
                    response.AccountNumber = e.AccountNumber;
                    break;
                case DBConcurrencyException e:
                    accountNumber = context.Session.GetInt32("AccountNumber") ?? 0;
                    response.AccountNumber = accountNumber;
                    response.Message = ErrorCode.E2.GetDisplayAttribute().Name;
                    break;
                //Exception by system
                default:
                    accountNumber = context.Session.GetInt32("AccountNumber") ?? 0;
                    response.AccountNumber = accountNumber;
                    response.Message = ErrorCode.E0.GetDisplayAttribute().Name;
                    Log.Error(exception, string.Empty, accountNumber);
                    break;
            }

            transactionService.InsertTransaction(response);
            context.Response.ContentType = "application/json";

            return context.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }
    }
}
