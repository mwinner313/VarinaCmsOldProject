using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.DomainClasses.Users;

namespace VarinaCmsV2.Security.Contracts
{
    public interface IUserManagerSeedDataBaseContext
    {
        List<Role> InitialRoles { get; set; }
        List<User> InitialUsers { get; set; }
        Task PostDataInitializeAsync(IAppUserManager userManager);
    }
}
