using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using VarinaCmsV2.Common.WebApi;
using VarinaCmsV2.Core.Security.Premissions;
using VarinaCmsV2.Core.Services;
using VarinaCmsV2.Core.Services.Widget;
using VarinaCmsV2.Core.Web.Security.WebApi;
using VarinaCmsV2.ViewModel.Widget;

namespace VarinaCmsV2.Web.Areas.Admin.Controllers.Api
{
    [WebApiCmsAuthorize(Premissions = WidgetPremission.CanSee)]
    public class WidgetContainerController : BaseApiController
    {
        private readonly IWidgetContainerService _wdgetContainerService;

        public WidgetContainerController(IWidgetContainerService wdgetContainerService)
        {
            _wdgetContainerService = wdgetContainerService;
        }

        public async Task< IHttpActionResult> Get()
        {
            var serviceRes = await _wdgetContainerService.Get(new WidgetContainerGetListRequest()
            {
                RequestOwner = User
            });

            if(serviceRes.Access==ResponseAccess.Granted)
            return Ok(serviceRes);

            if (serviceRes.Access == ResponseAccess.Deny) return Unauthorized(serviceRes.Message);

            return BadRequest();
        }

        [WebApiCmsAuthorize(Premissions = WidgetPremission.CanAdd)]
        public async Task<IHttpActionResult> Post(WidgetContainerAddOrUpdateViewModel model)
        {
            var serviceRes = await _wdgetContainerService.Add(new WidgetContainerAddRequest()
            {
                RequestOwner = User,
                Model = model
            });

            if (serviceRes.Access == ResponseAccess.Granted)
                return Ok(serviceRes.Model);

            if (serviceRes.Access == ResponseAccess.Deny) return Unauthorized(serviceRes.Message);

            return BadRequest();
        }

        [WebApiCmsAuthorize(Premissions = WidgetPremission.CanEdit)]
        public async Task<IHttpActionResult> Put(Guid id,[FromBody] WidgetContainerAddOrUpdateViewModel model)
        {
            if(id!= model.Id)return  BadRequest();

            var serviceRes = await _wdgetContainerService.Edit(new WidgetContainerEditRequest()
            {
                RequestOwner = User,
                Model = model,
                WidgetContainerId = id
            });

            if (serviceRes.Access == ResponseAccess.Granted)
                return Ok(serviceRes);

            if (serviceRes.Access == ResponseAccess.Deny) return Unauthorized(serviceRes.Message);

            return BadRequest();
        }

        [WebApiCmsAuthorize(Premissions = WidgetPremission.CanDelete)]
        public async Task<IHttpActionResult> Delete(Guid id)
        {
            var serviceRes = await _wdgetContainerService.Delete(new WidgetContainerDeleteRequest()
            {
                RequestOwner = User,
                WidgetContainerId = id
            });

            if (serviceRes.Access == ResponseAccess.Granted)
                return Ok();

            if (serviceRes.Access == ResponseAccess.Deny) return Unauthorized(serviceRes.Message);

            return BadRequest();
        }
    }
}
