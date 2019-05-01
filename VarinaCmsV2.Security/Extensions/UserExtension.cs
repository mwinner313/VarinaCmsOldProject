//using System;
//using System.Security.Claims;
//using System.Security.Principal;
//using System.Threading.Tasks;
//using Microsoft.AspNet.Identity;
//using VarinaCmsV2.DomainClasses.Users;
//using VarinaCmsV2.Security.Contracts;

//namespace VarinaCmsV2.Security.Extensions
//{
//    public static class  UserExtension
//    {
//        public static async Task<ClaimsIdentity> GenerateUserIdentityAsync(this User user, IAppUserManager manager)
//        {
//            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
//            var userIdentity = await manager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
//            // Add custom user claims here
//            return userIdentity;
//        }
//        public static Guid GetUserId(this IIdentity identity)
//        {
//            if (identity == null)
//                throw new ArgumentNullException("identity");
//            ClaimsIdentity identity1 = identity as ClaimsIdentity;
//            if (identity1 != null)
//                return
//                    Guid.Parse(
//                        identity1.FindFirstValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"));
//            return Guid.Empty;
//        }
//    }
//}
