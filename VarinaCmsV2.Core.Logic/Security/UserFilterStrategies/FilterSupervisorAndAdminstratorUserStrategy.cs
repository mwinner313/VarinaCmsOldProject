using System.Linq;
using VarinaCmsV2.Common;
using VarinaCmsV2.Core.Contracts.Security;
using VarinaCmsV2.Core.Security.Premissions;
using VarinaCmsV2.DomainClasses.Users;
using VarinaCmsV2.Security.Contracts;

namespace VarinaCmsV2.Core.Logic.Security.UserFilterStrategies
{
    internal class FilterSupervisorAndAdminstratorUserStrategy : IUserFilterStrategy
    {
        private readonly IAppRoleManager _roleManager;
        private readonly FilterSupervisorUserStrategy _supervisorUserStrategy;
        public FilterSupervisorAndAdminstratorUserStrategy(IAppRoleManager roleManager)
        {
            _roleManager = roleManager;
            _supervisorUserStrategy=new FilterSupervisorUserStrategy(_roleManager);

        }

        public IQueryable<User> Filter(IQueryable<User> users)
        {
            var principalAdminRole =AsyncHelper.RunTaskSync(_roleManager.FindByNameAsync(PreDefRoles.PrincipalAdministrator));
            var adminRole =AsyncHelper.RunTaskSync(_roleManager.FindByNameAsync(PreDefRoles.Administrator));
            return
                _supervisorUserStrategy.Filter(users)
                    .Where(x => x.Roles.All(r => r.RoleId != principalAdminRole.Id))
                    .Where(x => x.Roles.All(r => r.RoleId != adminRole.Id));
        }
    }
}