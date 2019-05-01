using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using VarinaCmsV2.Common;
using VarinaCmsV2.Core.Contracts;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.Core.Security.Premissions;
using VarinaCmsV2.Core.Services.Settings;
using VarinaCmsV2.Core.Settings;
using VarinaCmsV2.Core.Web.Security.Mvc;
using VarinaCmsV2.DomainClasses.Users;
using VarinaCmsV2.Security.Contracts;
using VarinaCmsV2.ViewModel.User.Account;
using VarinaCmsV2.Web.Models;

namespace VarinaCmsV2.Web.Controllers
{
    [MvcCmsAuthorize]
    public class AccountController : Controller
    {
        private const string GuestUserIdCookieName = "GuestUser-Id";
        private readonly IAppRoleManager _roleManager;
        private readonly ISettingService _settingService;
        private readonly IAppSignInManager _signInManager;
        private readonly IUserDataService _userDataService;
        private readonly IAppUserManager _userManager;
        private readonly IWorkContext _workContext;

        public AccountController(IAppRoleManager roleManager, ISettingService settingService,
            IUserDataService userDataService, IAppUserManager userManager, IAppSignInManager signInManager,
            IWorkContext workContext)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _workContext = workContext;
            _userDataService = userDataService;
            _settingService = settingService;
            _roleManager = roleManager;
        }


        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        [Common.WebApi.ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

            switch (result)
            {
                case SignInStatus.Success:
                    IfUserHasAnOtherRecordInDataBaseAddCartItemsToRealAccountAndDeleteIt();
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new {ReturnUrl = returnUrl, model.RememberMe});
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        private void IfUserHasAnOtherRecordInDataBaseAddCartItemsToRealAccountAndDeleteIt()
        {
            var guestUserId = HttpContext.Request.Cookies[GuestUserIdCookieName]?.ToString().ToGuid();
            if (guestUserId.HasValue)
            {
                var existingUser = _userDataService.Query.Include(x => x.ShoppingCartItems)
                    .FirstOrDefault(x => x.Id == guestUserId);
                HttpContext.Request.Cookies[GuestUserIdCookieName].Expires = DateTime.Now.AddDays(-1);
                if (existingUser == null)
                    return;

                existingUser.ShoppingCartItems.ToList().ForEach(_workContext.CurrentUser.ShoppingCartItems.Add);
                _userDataService.DeleteAsync(existingUser.Id);
            }
        }

        public async Task<ActionResult> Register(string returnUrl)
        {
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        [Common.WebApi.ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(SignUpViewModel model)
        {
            if (User.Identity.IsAuthenticated) return RedirectWhereUserWantsToGo(model.ReturnUrl);
            if (ModelState.IsValid)
            {
                var user = InitalizeUserFromWorkContext(model);
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    await HandleRoles(user);
                    await _signInManager.SignInAsync(user, false, false);
                    await SendVerficationMessage(model, user);
                    ExpireGuestRoleCookie();
                    return RedirectWhereUserWantsToGo(model.ReturnUrl);
                }
                AddErrors(result);
            }
            return View(model);
        }

        private async Task HandleRoles(User user)
        {
            await ChangeBaseRole(user);
            await AddWebSitePolicyDefaultUserRoles(user);
        }

        private void ExpireGuestRoleCookie()
        {
            HttpContext.Request.Cookies[GuestUserIdCookieName].Expires = DateTime.Now.AddDays(-1);
        }

        private User InitalizeUserFromWorkContext(SignUpViewModel model)
        {
            var user = _workContext.CurrentUser;
            user.PasswordHash = _userManager.PasswordHasher.HashPassword(model.Password);
            user.UserName = model.EmailOrPhone;
            if (new EmailAddressAttribute().IsValid(model.EmailOrPhone))
                user.Email = model.EmailOrPhone;
            return user;
        }

        private async Task SendVerficationMessage(SignUpViewModel model, User user)
        {
            if (new EmailAddressAttribute().IsValid(model.EmailOrPhone))
            {
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user.Id);
                var callbackUrl = Url.Action("ConfirmEmail", "Account", new {userId = user.Id, code},
                    Request.Url.Scheme);
                await _userManager.SendEmailAsync(user.Id, "Confirm your account",
                    "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");
            }
        }

        private async Task AddWebSitePolicyDefaultUserRoles(User user)
        {
            foreach (var role in _settingService.GetSetting<WebSitePolicies>().DefaultNewUserRegisterRole)
                await _userManager.AddToRoleAsync(user.Id, (await _roleManager.FindByIdAsync(role.Id)).Name);
        }

        private async Task ChangeBaseRole(User user)
        {
            await _userManager.RemoveFromRoleAsync(user.Id, PreDefRoles.Guest);
            await _userManager.AddToRoleAsync(user.Id, PreDefRoles.WebSiteUser);
        }

        private ActionResult RedirectWhereUserWantsToGo(string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl)) return Redirect("/");
            return Redirect(returnUrl);
        }


        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.Email);
                if (user == null || !await _userManager.IsEmailConfirmedAsync(user.Id))
                    return View("ForgotPasswordConfirmation");

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                var code = await _userManager.GeneratePasswordResetTokenAsync(user.Id);
                var callbackUrl = Url.Action("ResetPassword", "Account", new {userId = user.Id, code},
                    Request.Url.Scheme);
                await _userManager.SendEmailAsync(user.Id, "Reset Password",
                    "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                return RedirectToAction("ForgotPasswordConfirmation");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await _signInManager.HasBeenVerifiedAsync())
                return View("Error");
            return View(new VerifyCodeViewModel {Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe});
        }


        [HttpPost]
        [AllowAnonymous]
        [Common.WebApi.ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await _signInManager.TwoFactorSignInAsync(model.Provider, model.Code, model.RememberMe,
                model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }


        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(Guid userId, string code)
        {
            if (userId == null || code == null)
                return View("Error");
            var result = await _userManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }


        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }


        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }


        [HttpPost]
        [AllowAnonymous]
        [Common.WebApi.ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var user = await _userManager.FindByNameAsync(model.Email);
            if (user == null)
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            var result = await _userManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            AddErrors(result);
            return View();
        }

        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [Common.WebApi.ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider,
                Url.Action("ExternalLoginCallback", "Account", new {ReturnUrl = returnUrl}));
        }

        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await _signInManager.GetVerifiedUserIdAsync();
            if (userId == null)
                return View("Error");
            var userFactors = await _userManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem {Text = purpose, Value = purpose})
                .ToList();
            return View(new SendCodeViewModel
            {
                Providers = factorOptions,
                ReturnUrl = returnUrl,
                RememberMe = rememberMe
            });
        }

        //
        [HttpPost]
        [AllowAnonymous]
        [Common.WebApi.ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
                return View();

            // Generate the token and send it
            if (!await _signInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
                return View("Error");
            return RedirectToAction("VerifyCode",
                new {Provider = model.SelectedProvider, model.ReturnUrl, model.RememberMe});
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
                return RedirectToAction("Login");

            // Sign in the user with this external login provider if the user already has a login
            var result = await _signInManager.ExternalSignInAsync(loginInfo, false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new {ReturnUrl = returnUrl, RememberMe = false});
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation",
                        new ExternalLoginConfirmationViewModel {Email = loginInfo.Email});
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [Common.WebApi.ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model,
            string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Manage");

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                    return View("ExternalLoginFailure");
                var user = new User {UserName = model.Email, Email = model.Email};
                var result = await _userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await _userManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, false, false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }


        [HttpGet]
        [MvcCmsAuthorize]
        public ActionResult SignOut()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }


        #region Helpers

        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
                ModelState.AddModelError("", error);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties {RedirectUri = RedirectUri};
                if (UserId != null)
                    properties.Dictionary[XsrfKey] = UserId;
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }

        #endregion
    }
}