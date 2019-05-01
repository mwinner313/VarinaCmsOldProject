using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VarinaCmsV2.Common
{
    public static class RegexPattern
    {
        /// <summary>
        /// YYYY/MM/DD
        /// </summary>
        public const string Date = @"^\d{4}/((0\d)|(1[012]))/(([012]\d)|3[01])$";
        /// <summary>
        /// YYYY/MM/DD - hh:mm
        /// </summary>
        public const string DateTime = @"^\d{4}/((0\d)|(1[012]))/(([012]\d)|3[01])\s*-\s*(([01]{0,1}[0-9])|2[0-4]):[0-5][0-9]$";
        /// <summary>
        /// ^[0-9]*$
        /// </summary>
        public const string Number = "^[0-9]*$";
    }
}
