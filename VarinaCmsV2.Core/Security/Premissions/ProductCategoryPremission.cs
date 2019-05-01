using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.Security.Contracts;

namespace VarinaCmsV2.Core.Security.Premissions
{
    public class ProductCategoryPremission: IPremissionList
    {
        public const string CanSee = "product-category.see";
        public const string CanAdd = "product-category.add";
        public const string CanEdit = "product-category.edit";
        public const string CanDelete = "product-category.delete";
        public List<string> List { get; } = new List<string>()
        {
            CanSee,
            CanAdd,
            CanEdit,
            CanDelete
        };
    }
}
