using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VarinaCmsV2.Common
{
    public static class StringExtensions
    {
        public static Guid ToGuid(this string str)
        {
            return Guid.Parse(str);
        }

        public static List<int> ParseNumbers(this string numbers)
        {
            if (string.IsNullOrEmpty(numbers)) return null;
            var retNums = new List<int>();
            foreach (var s in numbers.Split(','))
            {
                retNums.Add(int.Parse(s));
            }
            return retNums;
        }
    }
}
