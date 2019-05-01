//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Linq;
//using EntityFramework.DynamicFilters;
//using VarinaCmsV2.Common;
//using VarinaCmsV2.Data.DbContexts;
//using VarinaCmsV2.DomainClasses.Contracts;
//using VarinaCmsV2.DomainClasses.Users;
//using VarinaCmsV2.Security.Contracts;
//using VarinaCmsV2.Security.Extensions;

//namespace VarinaCmsV2.Core.Infrastructure
//{
//    public class RestrictedItemGlobalFilter : GlobalFilter
//    {
//        public IIdentityManager IdentityManager { get; set; }
//        public override void ApplyFilter(DbModelBuilder modelBuilder)
//        {
//            var userId = IdentityManager.GetCurrentIdentity().GetUserId();
//            modelBuilder.Filter("RestrictItemsBaseOnAccsess", 
//                (IAccessRestrictedItem x,IQueryable<Role> roles) => x.AccessType == AccessType.Public
//                ||IdentityManager.IsCurrentIdentitySuperAdmin()
//                         || roles.Any(r=> x.AllowedRolesPremissionJsonString.Contains(r.Name)),
//                 (AppDbContext ctx)=>ctx.Roles.Where(x=>x.Users.Any(r=>r.UserId== userId)));
//        }

//        private List<string> GetIdentityroles()
//        {
//            return IdentityManager.GetCurrentIdentityRoles();
//        }

//    }
//}
