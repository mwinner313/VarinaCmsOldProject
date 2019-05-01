using System.Linq;
using VarinaCmsV2.Common;
using VarinaCmsV2.Core.Contracts.Security;
using VarinaCmsV2.Core.Security.Premissions;
using VarinaCmsV2.DomainClasses.Users;
using VarinaCmsV2.Security.Contracts;

namespace VarinaCmsV2.Core.Logic.Security.UserFilterStrategies
{
    internal class FilterSupervisorUserStrategy : IUserFilterStrategy
    {
        private readonly IAppRoleManager _roleManager;

        public FilterSupervisorUserStrategy(IAppRoleManager roleManager)
        {
            _roleManager = roleManager;
        }

        public IQueryable<User> Filter(IQueryable<User> users)
        {
            var supervisorRole = AsyncHelper.RunTaskSync(_roleManager.FindByNameAsync(PreDefRoles.Supervisor));
            return users.Where(x => x.Roles.All(r=>r.RoleId!=supervisorRole.Id));
        }
    }
}