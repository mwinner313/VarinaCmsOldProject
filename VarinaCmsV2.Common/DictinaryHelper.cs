using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace VarinaCmsV2.Common
{
    public static class DictinaryHelper
    {
        public static void AddIfNotExist(this IDictionary dic, object key, object value)
        {
            if (dic.Contains(key)) return;
            dic.Add(key, value);
        }
        public static void AddReplaceIfExist(this IDictionary<object, object> dic, object key, object value)
        {
            if (dic.ContainsKey(key))
                dic.Remove(key);

            dic.Add(key, value);
        }
    }
}
