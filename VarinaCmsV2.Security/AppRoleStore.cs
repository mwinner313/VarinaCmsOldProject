using System;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using VarinaCmsV2.DomainClasses.Users;

namespace VarinaCmsV2.Security
{
    public class AppRoleStore:RoleStore<Role,Guid,UserRole>
    {
        public AppRoleStore(DbContext context) : base(context)
        {
           
        }
    }
}
