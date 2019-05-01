using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;

namespace VarinaCmsV2.Security.Contracts
{
    public interface IIdentityManager
    {
        List<string> GetCurrentIdentityRoles();
        List<string> GetCurrentIdentityPremissions();
        ClaimsIdentity GetCurrentIdentity();
        IPrincipal GetCurrentPrincipal();
        bool IsCurrentIdentitySupervisor();
        bool HasPremission(IPrincipal principal, string premissions);
        bool HasPremission(ClaimsIdentity principal, string premissions);
        bool CurrentIdentityHasOneOfRoles(IEnumerable<string> roles);
    }
}
