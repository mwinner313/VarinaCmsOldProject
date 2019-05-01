using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Threading.Tasks;
using System.Web.Http;
using VarinaCmsV2.Common.WebApi;
using VarinaCmsV2.Core.Web.Security.WebApi;
using VarinaCmsV2.DomainClasses.Users;
using VarinaCmsV2.Security.Contracts;
using VarinaCmsV2.ViewModel.User.Account;

namespace VarinaCmsV2.Web.Controllers.Api
{
    [System.Web.Http.RoutePrefix("api/account")]
    public class AccountController : BaseApiController
    {
        private readonly IAppSignInManager _signInManager;
        private readonly IAppUserManager _userManager;
        private readonly IAuthenticationManager _authenticationManager;
        public AccountController(IAppSignInManager signInManager, IAppUserManager userManager, IAuthenticationManager authenticationManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _authenticationManager = authenticationManager;
        }

        [Route("signin")]
        [AllowAnonymous]
        [HttpPost]
        //[Common.WebApi.ValidateAntiForgeryToken]
        public async Task<IHttpActionResult> SignIn(SignInViewModel model)
        {
            IHttpActionResult res = BadRequest();

            var signInResualt = await _signInManager.PasswordSignInAsync(model.EmailOrPhone, model.Password, model.RememberMe, shouldLockout: true);
            switch (signInResualt)
            {
                case SignInStatus.Success:
                    res = Ok();
                    break;
                case SignInStatus.Failure:
                case SignInStatus.LockedOut:
                    res = BadRequest(await _signInManager.GetSignInFailedAttempReason(model.EmailOrPhone, model.Password));
                    break;
            }
            return res;
        }

        [Route("signup")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IHttpActionResult> SignUp(SignUpViewModel model)
        {
            var res = await _userManager.CreateAsync(new User()
            {
                UserName = model.EmailOrPhone
            }, model.Password);

            if (res.Succeeded)
            {
                await _signInManager.PasswordSignInAsync(model.EmailOrPhone, model.Password, false, false);
                return Ok();
            }

            foreach (var err in res.Errors)
            {
                ModelState.AddModelError("",err);
            }
         
            return BadRequest(GetModelStateErorrs());
        }

        [HttpPost]
        [WebApiCmsAuthorize]
        public async Task<IHttpActionResult> SignOut()
        {
          _authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return Ok();
        }
    }
}
