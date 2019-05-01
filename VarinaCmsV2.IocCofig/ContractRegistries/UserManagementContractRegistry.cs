using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Newtonsoft.Json.Linq;
using StructureMap.Web;
using VarinaCmsV2.Common;
using VarinaCmsV2.Core.Logic.Helpers;
using VarinaCmsV2.Core.Web.Security.WebApi;
using VarinaCmsV2.Data.Contracts;
using VarinaCmsV2.DomainClasses.Entities;
using VarinaCmsV2.DomainClasses.Users;
using VarinaCmsV2.Security;
using VarinaCmsV2.Security.Contracts;

namespace VarinaCmsV2.IocCofig.ContractRegistries
{
    public class UserManagementContractRegistry : StructureMap.Registry
    {
        public UserManagementContractRegistry()
        {
            For<IAppUserManager>().HybridHttpOrThreadLocalScoped().Use<AppUserManager>();
            For<IEmailSecuriyService>().HybridHttpOrThreadLocalScoped().Use("get initial email acounct for IEmailService", c =>
            {
                var email = c.GetInstance<IUnitOfWork>().Set<MessageServiceAccount>().FirstOrDefault(x =>
                    x.Type == MessageServiceAccountTypeHelper.Email && x.IsDefaultForUse);
                return new Security.EmailSecuriyService(email?.MetaData.ToObject<EmailInfo>());
            });

            For<UserManager<User,Guid>>().HybridHttpOrThreadLocalScoped().Use<AppUserManager>();
            For<AppUserManager>().HybridHttpOrThreadLocalScoped().Use<AppUserManager>();
            For<IAppSignInManager>().HybridHttpOrThreadLocalScoped().Use<AppSignInManager>();
            For<IAppRoleManager>().HybridHttpOrThreadLocalScoped().Use<AppRoleManager>();
            For<IAuthenticationManager>().Use(() => HttpContext.Current.GetOwinContext().Authentication);
            For<IUserStore<User, Guid>>().HybridHttpOrThreadLocalScoped().Use<AppUserStore>();
            For<IRoleStore<Role, Guid>>().HybridHttpOrThreadLocalScoped().Use<AppRoleStore>();
            For<IIdentityManager>().HybridHttpOrThreadLocalScoped().Use<IdentityManager>();
            For<List<IPremissionList>>().Use("find all ipremission lists", TypeHelper.FindListOf<IPremissionList>);
            For<List<IRoleList>>().Singleton().Use("find all irole lists", TypeHelper.FindListOf<IRoleList>);

        }
    }
}