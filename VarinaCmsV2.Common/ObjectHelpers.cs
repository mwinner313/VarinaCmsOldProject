using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VarinaCmsV2.Common
{
    public static class ObjectHelpers
    {
        public static T Cast<T>(this object obj)
        {
            return (T) obj;
        }
    }
}
