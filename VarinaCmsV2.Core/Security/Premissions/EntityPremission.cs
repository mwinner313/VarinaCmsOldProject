using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.Security.Contracts;

namespace VarinaCmsV2.Core.Security.Premissions
{
    public class EntityPremission : IPremissionList
    {
        public const string CanAdd = "entity.add";
        public const string CanEdit = "entity.edit";
        public const string CanDelete = "entity.delete";
        public const string CanSee = "entity.see";
        public List<string> List { get; } = new List<string>()
        {
            CanSee,
            CanAdd,
            CanDelete,
            CanEdit
        };

    }
}
