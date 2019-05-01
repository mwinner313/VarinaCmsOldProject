using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.Core.Contracts.Security;
using VarinaCmsV2.Core.Contracts.WebClientServices;
using VarinaCmsV2.Core.Contracts.WebClientServices.Entities;
using VarinaCmsV2.Core.Logic.Helpers;
using VarinaCmsV2.Core.Web;
using VarinaCmsV2.Security.Contracts;

namespace VarinaCmsV2.Web.Controllers
{
    [System.Web.Mvc.AllowAnonymous]
    public class UserProfileController : LiquidController
    {
        private readonly IAppUserManager _userManager;
        private readonly IEntityWebClientService _entityService;
        public UserProfileController(IAppUserManager userManager, IEntityWebClientService entityService)
        {
            _userManager = userManager;
            _entityService = entityService;
        }

        public async Task<ActionResult> ShowUserProfileAsync(string userUrl)
        {
            var user = await _userManager.FindByUrlAsync(userUrl);
            return LiquidView(user.AsLiquidAdapted());
        }

      
    }
}
