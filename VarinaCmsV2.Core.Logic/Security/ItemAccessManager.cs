using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using VarinaCmsV2.Core.Contracts.Security;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.Security.Contracts;

namespace VarinaCmsV2.Core.Logic.Security
{
    //TODO test
    public class ItemAccessManager : IRestrictedItemAccessManager
    {
        private readonly IIdentityManager _identityManager;

        public ItemAccessManager(IIdentityManager identityManager)
        {
            _identityManager = identityManager;
        }

        public bool HasAccess(IAccessRestrictedItem restricted, AccessPremission premission)
        {
            if (restricted.AccessType == AccessType.Public) return true;
            if (_identityManager.IsCurrentIdentitySupervisor()) return true;
            var identityRoles = _identityManager.GetCurrentIdentityRoles();

            return
                restricted.AllowedRoles.Any(
                    x => x.AccessPremission == premission && identityRoles.Contains(x.RoleName));
        }

        public IQueryable<T> Filter<T>(IQueryable<T> items)where T: class ,IAccessRestrictedItem
        {
            var rolesWithAllowedActions = new List<string>();
            foreach (var role in _identityManager.GetCurrentIdentityRoles())
            {
                rolesWithAllowedActions.Add(JsonConvert.SerializeObject(new AllowedRole()
                {
                    AccessPremission = AccessPremission.See,
                    RoleName = role
                }));
            }
           return items.Where(x => x.AccessType == AccessType.Public    
                                    || rolesWithAllowedActions
                                         .Any(r => x.AllowedRolesString.Contains(r)));
        }
        
    }
}
