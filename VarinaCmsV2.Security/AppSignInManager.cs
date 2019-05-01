using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using VarinaCmsV2.DomainClasses.Users;
using VarinaCmsV2.Security.Contracts;
using VarinaCmsV2.Security.Extensions;

namespace VarinaCmsV2.Security
{
    public class AppSignInManager : SignInManager<User, Guid>, IAppSignInManager
    {

        private readonly IAppUserManager _userManager;
        public AppSignInManager(AppUserManager userManager, IAuthenticationManager authenticationManager) : base(userManager, authenticationManager)
        {
            _userManager = userManager;
        }
        public override Task<ClaimsIdentity> CreateUserIdentityAsync(User user)
        {
            return _userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="isPersistent">
        ///   Persistent cookies will be saved as files in the browser folders until they either expire or manually deleted. This will cause
        ///  the cookie to persist even if you close the browser.
        ///  If IsPersistent is set to false, the browser will acquire session cookie which gets cleared when the browser is closed.
        ///  Now the reason session cookie wont clear after restarting the browser is because of chrome default settings.To fix it go
        ///  to chrome settings -> advanced, and uncheck Continue running background apps when Google Chrome is closed under System section.
        /// </param>
        /// <param name="shouldLockout">
        /// If shouldLockout is true, user is getting locked out after a number of failed attempts to login.
        /// </param>
        /// <returns></returns>
        public override async Task<SignInStatus> PasswordSignInAsync(string userName, string password, bool isPersistent, bool shouldLockout)
        {
            if (await IsUserBanned(userName, password)) return SignInStatus.Failure;

            var result = await base.PasswordSignInAsync(userName, password, isPersistent, shouldLockout);

            return result == SignInStatus.Failure
                ? await SignInByUserPhoneNumber(userName, password, isPersistent, shouldLockout)
                : result;
        }

        private async Task<bool> IsUserBanned(string userName, string password)
        {
            var user = await _userManager.FindAsync(userName, password) ?? await _userManager.FindByPhoneNumberAsUserNameAsync(userName, password);
            return user?.IsBanned ?? false;
        }

        private async Task<SignInStatus> SignInByUserPhoneNumber(string userName, string password, bool isPersistent, bool shouldLockout)
        {
            var user = await _userManager.FindByPhoneNumberAsUserNameAsync(userName, password);
            if (user == null || user.IsBanned) return SignInStatus.Failure;
            else
                return await PasswordSignInAsync(user.UserName, password, isPersistent, shouldLockout);
        }

        public static AppSignInManager Create(IdentityFactoryOptions<AppSignInManager> options, IOwinContext context)
        {
            return new AppSignInManager(context.Get<AppUserManager>(), context.Authentication);
        }

        public async Task<string> GetSignInFailedAttempReason(string userName, string password)
        {
            var user = await UserManager.FindAsync(userName, password) ??
                      await _userManager.FindByPhoneNumberAsUserNameAsync(userName, password);
            if (user == null)
                return SignInManagerTexts.UserNameOrPasswordIsIncorrect;
            if (await _userManager.IsLockedOutAsync(user.Id))
                return string.Format(SignInManagerTexts.Lockout, user.LockoutEndDateUtc);
            if (user.IsBanned) return user.BanReason;
            return "";
        }
    }
}
