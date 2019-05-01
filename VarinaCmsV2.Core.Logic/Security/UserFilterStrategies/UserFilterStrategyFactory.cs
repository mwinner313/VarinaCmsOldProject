using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.Core.Contracts.Security;
using VarinaCmsV2.Core.Security.Premissions;
using VarinaCmsV2.Security.Contracts;

namespace VarinaCmsV2.Core.Logic.Security.UserFilterStrategies
{
    public class UserFilterStrategyFactory: IUserFilterStrategyFactory
    {
        private readonly IAppRoleManager _roleManager;

        public UserFilterStrategyFactory(IAppRoleManager roleManager)
        {
            _roleManager = roleManager;
        }

        public IUserFilterStrategy GetStrategy(IIdentity identity)
        {
            if (identity is ClaimsIdentity claimIdentity)
            {
                var identityRoles = claimIdentity.FindAll(ClaimTypes.Role).ToList();
                if (identityRoles.Any(x => x.Value == PreDefRoles.Supervisor)) return new NoUserFilterStrategy();
                if (identityRoles.Any(x => x.Value == PreDefRoles.PrincipalAdministrator)) return new FilterSupervisorUserStrategy(_roleManager);
                if (identityRoles.Any(x => x.Value == PreDefRoles.Administrator)) return new FilterSupervisorAndAdminstratorUserStrategy(_roleManager);
            }
            return null;
        }
    }
}
