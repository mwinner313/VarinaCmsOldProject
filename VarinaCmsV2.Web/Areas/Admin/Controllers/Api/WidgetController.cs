using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using VarinaCmsV2.Core.Security.Premissions;
using VarinaCmsV2.Core.Services;
using VarinaCmsV2.Core.Services.Widget;
using VarinaCmsV2.Core.Web.Security.WebApi;
using VarinaCmsV2.ViewModel.Widget;

namespace VarinaCmsV2.Web.Areas.Admin.Controllers.Api
{
    public class WidgetController : ApiController
    {
        private readonly IWidgetContainerService _widgetContainerService;

        public WidgetController(IWidgetContainerService widgetContainerService)
        {
            _widgetContainerService = widgetContainerService;
        }
        [WebApiCmsAuthorize(Premissions = WidgetPremission.CanAdd)]
        public async Task<IHttpActionResult> Post(WidgetAddOrUpdateViewModel model)
        {
            var serviceRes = await _widgetContainerService.AddWidget(new WidgetAddRequest()
            {
                RequestOwner = User,
                Model = model
            });
            if (serviceRes.Access == ResponseAccess.Granted) return Ok(serviceRes.Model);

            if (serviceRes.Access == ResponseAccess.Deny) return Unauthorized();

            return BadRequest();
        }

        [WebApiCmsAuthorize(Premissions = WidgetPremission.CanEdit)]
        public async Task<IHttpActionResult> Put(Guid id,[FromBody] WidgetAddOrUpdateViewModel model)
        {
            if (id != model.Id) return BadRequest("invalid uri id");

            var serviceRes = await _widgetContainerService.EditWidget(new WidgetEditRequest()
            {
                RequestOwner = User,
                Model = model,
                WidgetId = id
            });

            if (serviceRes.Access == ResponseAccess.Granted) return Ok(serviceRes.Model);

            if (serviceRes.Access == ResponseAccess.Deny) return Unauthorized();

            return BadRequest();
        }

        [WebApiCmsAuthorize(Premissions = WidgetPremission.CanDelete)]
        public async Task<IHttpActionResult> Delete(Guid id)
        {
            var serviceRes = await _widgetContainerService.DeleteWidget(new WidgetDeleteRequest()
            {
                RequestOwner = User,
                WidgetId = id
            });

            if (serviceRes.Access == ResponseAccess.Granted) return Ok(serviceRes.Model);

            if (serviceRes.Access == ResponseAccess.Deny) return Unauthorized();

            return BadRequest();
        }
    }
}
