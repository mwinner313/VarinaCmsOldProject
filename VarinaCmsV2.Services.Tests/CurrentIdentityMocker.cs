using System;
using System.IO;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;

namespace VarinaCmsV2.Services.Tests
{
    public static class CurrentIdentityMocker
    {
        private  static ClaimsIdentity _identity;
        public static ClaimsIdentity Get()
        {
            if (_identity != null) return _identity;
            _identity = new ClaimsIdentity("authenticatd");
            _identity.AddClaim(new Claim(ClaimTypes.NameIdentifier,Guid.NewGuid().ToString()));
            Thread.CurrentPrincipal=new ClaimsPrincipal(_identity);
            return _identity;
        }
    }
}
