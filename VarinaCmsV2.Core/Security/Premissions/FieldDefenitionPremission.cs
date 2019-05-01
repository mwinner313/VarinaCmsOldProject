using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.Security.Contracts;

namespace VarinaCmsV2.Core.Security.Premissions
{
    public class FieldDefenitionPremission:IPremissionList
    {
        public const string CanManage = "entity.manage";
        public const string CanSee = "entity.see";
        public List<string> List { get; } = new List<string>()
        {
            CanSee,
            CanManage,
        };
    }
}
