using System.Threading.Tasks;
using System.Web.Mvc;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.Core.Contracts.Security;
using VarinaCmsV2.Core.Contracts.WebClientServices.Pages;
using VarinaCmsV2.Core.Logic.Helpers;
using VarinaCmsV2.Core.Web;
using VarinaCmsV2.DomainClasses.Contracts;

namespace VarinaCmsV2.Web.Controllers
{
    public class PageController:LiquidController
    {
        private readonly IPageWebClientService _pageClientService;
        private readonly IRestrictedItemAccessManager _accessManager;
        public PageController(IPageWebClientService pageClientService, IRestrictedItemAccessManager accessManager)
        {
            _pageClientService = pageClientService;
            _accessManager = accessManager;
        }

        [AllowAnonymous]
        public async Task<ActionResult> ShowPageAsync(string[] pageUrl)
        {
            var page = await _pageClientService.GetByUrl(string.Join("/", pageUrl));
            if (page == null) return LiquidNotFoundView();
            if(!_accessManager.HasAccess(page,AccessPremission.See))return new HttpUnauthorizedResult();

            return LiquidView(page.AdaptToLiquid());
        }
    }
}