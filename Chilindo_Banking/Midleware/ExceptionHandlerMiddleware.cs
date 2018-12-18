using Chilindo_Data.Helper;
using Chilindo_Database.ViewModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Globalization;
using System.Threading.Tasks;
using Chilindo_Data.Data;
using Chilindo_Database.Entity;
using ChilinDo_Service.Interface;

namespace Chilindo_Banking.Midleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, ICommonService commonService)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(context, exception, commonService);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception, ICommonService commonService)
        {
            var respone = new TransactionBaseResponse
            {
                Successful = false
            };

            switch (exception)
            {
                //Custom error showing to user
                case CustomException e:
                    respone.Message = e.ErrorMessage;
                    respone.AccountNumber = e.AccountNumber;
                    commonService.InsertTransactionHistory(respone);
                    break;
                //Exception by system
                default:
                    var accountNumber = context.Session.GetInt32("AccountNumber");
                    respone.AccountNumber = accountNumber ?? 0;
                    respone.Message = exception.Message;
                    commonService.InsertTransactionHistory(respone);

                    //Update message show to end user
                    respone.Message = ErrorCode.E1.GetDisplayAttribute().Name;
                    break;
            }
            context.Response.ContentType = "application/json";

            return context.Response.WriteAsync(respone.JsonSerialize());
        }
    }
}
