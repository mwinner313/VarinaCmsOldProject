using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using VarinaCmsV2.Security.Contracts;

namespace VarinaCmsV2.Security
{
    public class IdentityManager : IIdentityManager
    {
        private readonly List<string> _allSystemPremissions;
        private readonly List<string> _allSystemRoles;
        public IdentityManager(List<IPremissionList> allSystemPremissions, List<IRoleList> allSystemRoles)
        {
            _allSystemPremissions = allSystemPremissions.SelectMany(x => x.List).ToList();
            _allSystemRoles = allSystemRoles.SelectMany(x => x.List).ToList();
        }
        public List<string> GetCurrentIdentityRoles()
        {
            return GetCurrentIdentity().FindAll(ClaimTypes.Role).Select(x => x.Value).ToList();
        }

        public List<string> GetCurrentIdentityPremissions()
        {
            return IsCurrentIdentitySupervisor()
                ? _allSystemPremissions
                : GetCurrentIdentity().FindAll(CustomClaimTypes.Premission).Select(x => x.Value).ToList();
        }

        public ClaimsIdentity GetCurrentIdentity()
        {
            var identity = Thread.CurrentPrincipal.Identity as ClaimsIdentity ??
                           HttpContext.Current?.User?.Identity as ClaimsIdentity;
            return identity;
        }

        public IPrincipal GetCurrentPrincipal()
        {
            return Thread.CurrentPrincipal;
        }

        public bool IsCurrentIdentitySupervisor()
        {
            return GetCurrentIdentity().FindAll(ClaimTypes.Role).Any(x => x.Value == "Supervisor");
        }

        public bool HasPremission(IPrincipal principal, string premissions)
        {
            var user = principal.Identity as ClaimsIdentity;
            if (user == null) throw new ArgumentException(nameof(principal));
            return HasPremission(user, premissions);
        }

        public bool HasPremission(ClaimsIdentity identity, string premissions)
        {
            return identity.FindAll(ClaimTypes.Role).Any(x => x.Value == "Supervisor")|| identity.FindAll(CustomClaimTypes.Premission).Any(x => premissions.Contains(x.Value));
        }

        public bool CurrentIdentityHasOneOfRoles(IEnumerable<string> roles)
        {
            return GetCurrentIdentityRoles().Any(roles.Contains);
        }
    }
}
