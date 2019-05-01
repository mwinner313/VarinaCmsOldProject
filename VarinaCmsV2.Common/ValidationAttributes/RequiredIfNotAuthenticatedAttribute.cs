using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace VarinaCmsV2.Common.ValidationAttributes
{
    public class RequiredIfNotAuthenticatedAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return ((value is string) && string.IsNullOrEmpty(value.ToString())) || Thread.CurrentPrincipal.Identity.IsAuthenticated;
        }
    }
}
