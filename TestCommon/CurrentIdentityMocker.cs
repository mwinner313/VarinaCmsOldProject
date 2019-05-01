using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestCommon
{
    public static class CurrentIdentityMocker
    {
        public static void Create()
        {
            var identity = new ClaimsIdentity();
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier,Guid.NewGuid().ToString()));
            Thread.CurrentPrincipal=new ClaimsPrincipal(identity);
        }
    }
}
