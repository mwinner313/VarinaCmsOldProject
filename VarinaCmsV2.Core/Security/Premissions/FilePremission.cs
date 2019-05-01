using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.Security.Contracts;

namespace VarinaCmsV2.Core.Security.Premissions
{
    public class FilePremission:IPremissionList
    {
        public const string CanSee = "file.see";
        public const string CanAdd = "file.add";
        public const string CanUpdate = "file.update";
        public const string CanDelete = "file.delete";
        public List<string> List { get; } = new List<string>()
        {
          CanSee,
          CanAdd,
          CanUpdate,
          CanDelete
        };
    }
}
