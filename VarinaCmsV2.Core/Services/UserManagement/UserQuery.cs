using System;
using System.Collections.Generic;
using VarinaCmsV2.Common;

namespace VarinaCmsV2.Core.Services.UserManagement
{
    public class UserQuery:Pagenation
    {
        public string SearchText { get; set; }
        public List<Guid> RoleIds { get; set; }=new List<Guid>();
        public bool IsBanned { get; set; }
    }
}