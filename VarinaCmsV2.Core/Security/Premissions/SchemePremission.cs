using System.Collections.Generic;
using VarinaCmsV2.Security.Contracts;

namespace VarinaCmsV2.Core.Security.Premissions
{
    public class SchemePremission : IPremissionList
    {
        public const string CanSee = "entityscheme.show";
        public const string CanManage = "entityscheme.manage";
        public List<string> List { get; set; } = new List<string>()
        {
            CanManage,CanSee
        };
    }
}
