using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using VarinaCmsV2.Common.WebApi;
using VarinaCmsV2.Core.Security.Premissions;
using VarinaCmsV2.Core.Services.Dashboard;
using VarinaCmsV2.Core.Web.Security.WebApi;

namespace VarinaCmsV2.Web.Areas.Admin.Controllers.Api
{
    [WebApiCmsAuthorize(Premissions = PanelPremission.Default)]
    public class DashboardController : BaseApiController
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }
        public async Task<IHttpActionResult> Get()
        {
            return Ok(_dashboardService.GetDashboardWidgetsData());
        }
    }
}
