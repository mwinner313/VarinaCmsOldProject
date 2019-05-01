using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.Security.Contracts;

namespace VarinaCmsV2.Core.Security.Premissions
{
    public class OrderPremission : IPremissionList
    {

        public const string CanSee = "order.see";
        public const string CanAdd = "order.add";
        public const string CanEdit = "order.edit";
        public const string CanDelete = "order.delete";
        public List<string> List { get; } = new List<string>()
        {
            CanSee         ,
            CanAdd         ,
            CanEdit        ,
            CanDelete
        };
    }
}
