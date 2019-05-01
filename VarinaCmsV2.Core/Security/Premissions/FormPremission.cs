using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.Security.Contracts;

namespace VarinaCmsV2.Core.Security.Premissions
{
    public class FormPremission:IPremissionList
    {

        public const string CanSee = "form.see";
        public const string CanAdd = "form.add";
        public const string CanUpdate = "form.update";
        public const string CanDelete = "form.delete";
        public List<string> List { get; } = new List<string>()
        {
          CanSee,
          CanAdd,
          CanUpdate,
          CanDelete
        };
    }
}
