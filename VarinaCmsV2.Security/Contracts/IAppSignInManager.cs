using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using VarinaCmsV2.DomainClasses.Users;

namespace VarinaCmsV2.Security.Contracts
{
    public interface IAppSignInManager:IDisposable
    {
        Task<ClaimsIdentity> CreateUserIdentityAsync(User user);
        Task<SignInStatus> PasswordSignInAsync(string userName, string password, bool isPersistent, bool shouldLockout);
        string ConvertIdToString(Guid id);
        Guid ConvertIdFromString(string id);
        Task SignInAsync(User user, bool isPersistent, bool rememberBrowser);
        Task<bool> SendTwoFactorCodeAsync(string provider);
        Task<Guid> GetVerifiedUserIdAsync();
        Task<bool> HasBeenVerifiedAsync();
        Task<SignInStatus> TwoFactorSignInAsync(string provider, string code, bool isPersistent, bool rememberBrowser);
        Task<SignInStatus> ExternalSignInAsync(ExternalLoginInfo loginInfo, bool isPersistent);
        string AuthenticationType { get; set; }
        UserManager<User, Guid> UserManager { get; set; }
        IAuthenticationManager AuthenticationManager { get; set; }

        Task<string> GetSignInFailedAttempReason(string userName, string password);
    }
}