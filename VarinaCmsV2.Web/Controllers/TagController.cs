using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.Core.Contracts.Security;
using VarinaCmsV2.Core.Contracts.WebClientServices.Pages;
using VarinaCmsV2.Core.Logic.Helpers;
using VarinaCmsV2.Core.Web;
using VarinaCmsV2.ViewModel.Page;

namespace VarinaCmsV2.Web.Controllers
{
    [AllowAnonymous]
    public class TagController:LiquidController
    {
        private readonly ITagDataService _tagDataService;
        private readonly IRestrictedItemAccessManager _accessManager;
        private readonly IPageWebClientService _pageDataService;
        public TagController(ITagDataService tagDataService, IPageWebClientService pageDataService) 
        {
            _tagDataService = tagDataService;
            _pageDataService = pageDataService;
        }

        public async Task<ActionResult> ListByTagAsync(string tagUrl)
        {
            var tag = await _tagDataService.GetByUrlAsync(tagUrl);
            var pages = await _pageDataService.GetByTagAsync(tag.Url);
            return LiquidView(tag.AsLiquidAdaptedWithPages(pages));
        }
    }
}