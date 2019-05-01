using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VarinaCmsV2.Common.MVC
{
    public class EmailOrPhoneAttribute:ValidationAttribute
    {
        readonly EmailAddressAttribute _emailAttr=new EmailAddressAttribute();
        readonly PhoneAttribute _phoneAttr=new PhoneAttribute();

        public override bool IsValid(object value)
        {
            return _emailAttr.IsValid(value) || _phoneAttr.IsValid(value);
        }
    }
}
