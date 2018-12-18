using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Chilindo_Data.Data
{
    public enum ErrorCode
    {
        [Display(Name = "Internal Server Error")]
        E0,
        [Display(Name = "The Balance is not enough for withdrawal")]
        E1,
        [Display(Name = "This balance is changed, please proccess again")]
        E2,
    }
}
