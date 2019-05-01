using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.Core.Security.Premissions;
using VarinaCmsV2.Security;

namespace VarinaCmsV2.Core.Logic.Tests
{
    public static class Helper
    {
        public static IPrincipal GetMockPrincipal()
        {
            var identity = new ClaimsIdentity();
            identity.AddClaim(new Claim(CustomClaimTypes.Premission, PagePremision.CanSee));
            identity.AddClaim(new Claim(ClaimTypes.Role, "superviser"));
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, "12EA9E35-6B19-44D2-981C-2A8A3E6318E7"));
            return new ClaimsPrincipal(identity);
        }
    }
}
