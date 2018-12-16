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
        [Display(Name = "Bucket {0} is not exist")]
        E1,
    }
}
