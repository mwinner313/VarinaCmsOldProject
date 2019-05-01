using System.ComponentModel.DataAnnotations;

namespace VarinaCmsV2.Common.ValidationAttributes
{
    public class NoSpaceAttribute:ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return value==null || !value.ToString().Contains(" ");
        }
    }
}