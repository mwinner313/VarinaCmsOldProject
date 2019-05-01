using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using VarinaCmsV2.Data.DbContexts;
using VarinaCmsV2.DomainClasses.Entities;
using VarinaCmsV2.DomainClasses.Users;

namespace VarinaCmsV2.Data.DatabaseInitializers
{
    public class CustomDropCreateDatabaseIfModelChanges:DropCreateDatabaseIfModelChanges<AppDbContext>
    {
        protected override void Seed(AppDbContext context)
        {
            var userManager = new UserManager<User, Guid>(new UserStore<User, Role, Guid, UserLogin, UserRole, UserClaim>(context));
            var user = new User()
            {
                UserName = "mohammadali",
                Email = "m.ghanbari01375@gmail.com",
                Id = Guid.NewGuid(),
            };
            userManager.Create(user, "123456");

            context.Set<Language>().Add(new Language() {Name = "fa-Ir", ResourceAddress = "das"});
            context.SaveChanges();
            base.Seed(context);
           
        }
    }
}
