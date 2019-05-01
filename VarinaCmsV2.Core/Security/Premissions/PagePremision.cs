using System.Collections.Generic;
using VarinaCmsV2.Security.Contracts;

namespace VarinaCmsV2.Core.Security.Premissions
{
    public class PagePremision:IPremissionList
    {
        public const string CanSee= "page.show";
        public const string CanManage="page.manage";
        public List<string> List { get; set; } = new List<string>()
        {
            CanManage,CanSee
        };
    }
}
