using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Chilindo_Data.Data;
using Chilindo_Data.Helper;

namespace Chilindo_Data.Helper
{
    public class CustomException : Exception
    {
        public string ErrorCode { get; }
        public string ErrorMessage { get; }
        public int AccountNumber { get; }
        public CustomException(ErrorCode error, int accountNumber)
        {
            ErrorCode = error.ToString();
            ErrorMessage = error.GetDisplayAttribute().Name;
            AccountNumber = accountNumber;
        }
    }
}
