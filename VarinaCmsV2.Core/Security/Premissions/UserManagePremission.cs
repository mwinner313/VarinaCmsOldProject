using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.Security.Contracts;

namespace VarinaCmsV2.Core.Security.Premissions
{
    public class UserManagePremission:IPremissionList
    {
        public const string CanSeeUsers = "users.see";
        public const string CanAdd = "users.add";
        public const string CanDelete = "users.delete";
        public const string CanUpdate = "users.update";
        public List<string> List { get; }=new List<string>()
        {
            CanSeeUsers,
            CanAdd,
            CanDelete,
            CanUpdate
        };
    }
}
