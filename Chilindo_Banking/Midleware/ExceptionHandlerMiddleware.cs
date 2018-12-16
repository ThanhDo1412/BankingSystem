using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Chilindo_Data.Helper;
using Chilindo_Database.ViewModel;

namespace Chilindo_Banking.Midleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(context, exception);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var respone = new TransactionBaseResponse
            {
                Successful = false
            };

            switch (exception)
            {
                //Custom error showing to user
                case CustomError e:
                    respone.Message = e.ErrorMessage;
                    respone.AccountNumber = e.AccountNumber;
                    break;
                //Exception by system
                default:
                    break;
            }
            context.Response.ContentType = "application/json";

            return context.Response.WriteAsync(respone.JsonSerialize());
        }
    }
}
