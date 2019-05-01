using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.Security.Contracts;

namespace VarinaCmsV2.Core.Security.Premissions
{
    public class ProductPremission:IPremissionList
    {
        public const string CanSee = "product.see";
        public const string CanAdd = "product.add";
        public const string CanUpdate = "product.update";
        public const string CanDelete = "product.delete";
        public List<string> List { get; } = new List<string>()
        {
            CanSee,
            CanAdd,
            CanUpdate,
            CanDelete
        };
    }
}
