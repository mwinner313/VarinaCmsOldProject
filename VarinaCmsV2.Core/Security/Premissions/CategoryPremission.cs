using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.Security.Contracts;

namespace VarinaCmsV2.Core.Security.Premissions
{
    public class CategoryPremission : IPremissionList
    {

        public const string CanSee = "category.see";
        public const string CanAdd = "category.add";
        public const string CanEdit = "category.edit";
        public const string CanDelete = "category.delete";
        public List<string> List { get; } = new List<string>()
        {
            CanSee         ,
            CanAdd         ,
            CanEdit        ,
            CanDelete
        };
    }
}
