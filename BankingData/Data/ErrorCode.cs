using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BankingData.Data
{
    public enum ErrorCode
    {
        [Display(Name = "Internal Server Error")]
        E0,
        [Display(Name = "The Balance is not enough for withdrawal")]
        E1,
        [Display(Name = "This balance is changed, please proccess again")]
        E2,
        [Display(Name = "Account number does not existed")]
        E3,
        [Display(Name = "This account doesn't have this currency. Please try with another currency")]
        E4,
        [Display(Name = "Account Number can not be negative")]
        E5,
        [Display(Name = "Amount can not be negative")]
        E6,
        [Display(Name = "Currency can not be null or empty")]
        E7
    }
}
