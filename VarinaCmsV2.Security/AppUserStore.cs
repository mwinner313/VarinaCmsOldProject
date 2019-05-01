using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using VarinaCmsV2.Data.DbContexts;
using VarinaCmsV2.DomainClasses.Users;

namespace VarinaCmsV2.Security
{
    public class AppUserStore:UserStore<User,Role,Guid,UserLogin,UserRole,UserClaim>
    {
        public AppUserStore(AppDbContext context) : base(context)
        {
        }

       
    }
}
