using System;
using System.Reflection;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace BankingData.Helper
{
    public static class EnumHelper
    {
        public static DisplayAttribute GetDisplayAttribute(this Enum enumValue)
        {
            return enumValue.GetType()
                .GetMember(enumValue.ToString())
                .First()
                .GetCustomAttribute<DisplayAttribute>();
        }
    }
}
