using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace VarinaCmsV2.Common
{
    public class UrlBuilder:IUrlBuilder
    {
        public string GenrateUrlSegment(string str)
        {
            // invalid chars           
            str = Regex.Replace(str.ToLower(), @"[^آ-یA-Za-z0-9\s-]", "");
            // convert multiple spaces into one space   
            str = Regex.Replace(str, @"\s+", " ").Trim();
            // cut and trim 
            str = Regex.Replace(str, @"\s", "-"); // hyphens   
            str = Regex.Replace(str, @"-+", "-");
            return str;
        }
    }
}
