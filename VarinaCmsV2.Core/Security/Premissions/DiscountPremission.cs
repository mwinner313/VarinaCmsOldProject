using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.Security.Contracts;

namespace VarinaCmsV2.Core.Security.Premissions
{
    public class DiscountPremission:IPremissionList
    {
        public const string CanAdd = "discount.add";
        public const string CanEdit = "discount.edit";
        public const string CanDelete = "discount.delete";
        public const string CanSee = "discount.see";
        public List<string> List { get; } = new List<string>()
        {
            CanSee,
            CanAdd,
            CanDelete,
            CanEdit
        };

    }
}
