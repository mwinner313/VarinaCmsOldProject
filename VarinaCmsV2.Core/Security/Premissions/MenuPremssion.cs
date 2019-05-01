using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.Security.Contracts;

namespace VarinaCmsV2.Core.Security.Premissions
{
    public class MenuPremssion:IPremissionList
    {
        public const string CanSee = "menu.see";
        public const string CanAdd = "menu.add";
        public const string CanUpdate = "menu.update";
        public const string CanDelete = "menu.delete";
        public List<string> List { get; }=new List<string>()
        {
          CanSee,
          CanAdd,
          CanUpdate,
          CanDelete  
        };
    }
}
