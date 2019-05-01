using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using VarinaCmsV2.Common.Web;
using VarinaCmsV2.Core.FrontEndOptions;
using VarinaCmsV2.Core.Logic.Helpers;
using VarinaCmsV2.Core.Services.CurrentUser;
using VarinaCmsV2.Core.Web;
using VarinaCmsV2.Core.Web.Security.Mvc;

namespace VarinaCmsV2.Web.Controllers
{
    [MvcCmsAuthorize]
    [DenySearchEngineRequest]
    public class ProfileController : LiquidController
    {
        private readonly ICurrentUserService _currentUserService;
        public ProfileController(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }
        public async Task<ActionResult> Orders(int pageNumber)
        {
            var orders =
                await _currentUserService.GetOrders(pageNumber, FrontEndDeveloperOptions.Instance.Pagination.Default);
            return LiquidView(orders.MapToLiquidViewModel(), "orders");
        }
        public async Task<ActionResult> Addresses()
        {
            var addresses =
                await _currentUserService.GetAddresses();
            return LiquidView(addresses.MapToLiquidViewModel(), "addresses");
        }

    }
}