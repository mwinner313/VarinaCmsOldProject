using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using VarinaCmsV2.Common.WebApi;
using VarinaCmsV2.Core.Security;
using VarinaCmsV2.Core.Security.Premissions;
using VarinaCmsV2.Core.Services;
using VarinaCmsV2.Core.Services.Page;
using VarinaCmsV2.Core.Web.Security.WebApi;
using VarinaCmsV2.ViewModel.Page;

namespace VarinaCmsV2.Web.Areas.Admin.Controllers.Api
{
    [WebApiCmsAuthorize(Premissions = PanelPremission.Default)]
    public class PageController : BaseApiController
    {
        private readonly IPageService _pageService;

        public PageController(IPageService pageService)
        {
            _pageService = pageService;
        }
        [HttpGet]
        [WebApiCmsAuthorize(Premissions = PagePremision.CanSee)]
        public async Task<IHttpActionResult> Get(Guid id)
        {
            var serviceRes = await _pageService.Get(new PageRequest()
            {
                RequestOwner = User,
                PageId = id
            });

            IHttpActionResult res = BadRequest(serviceRes.Message);

            if (serviceRes.Access == ResponseAccess.Granted)
                res = Ok(serviceRes.Page);
            if (serviceRes.Access == ResponseAccess.Deny)
                res = Unauthorized(serviceRes.Message);

            return res;
        }
        [HttpGet]
        [WebApiCmsAuthorize(Premissions = PagePremision.CanSee)]
        public async Task<IHttpActionResult> Get([FromUri]PageQuery query)
        {
            var serviceRes = await _pageService.Get(new PageListRequest()
            {
                RequestOwner = User,
                Query = query
            });

            IHttpActionResult res = BadRequest();

            if (serviceRes.Access == ResponseAccess.Granted)
                res = Ok(new {items= serviceRes.Pages ,count=serviceRes.Count});
            if (serviceRes.Access == ResponseAccess.Deny)
                res = Unauthorized(serviceRes.Message);

            return res;
        }
        [HttpPost]
        [WebApiCmsAuthorize(Premissions = PagePremision.CanManage)]
        public async Task<IHttpActionResult> Post(PageAddUpdateViewModel model)
        {
            var serviceRes = await _pageService.Add(new PageAddRequest()
            {
                RequestOwner = User,
                ViewModel = model
            });
            IHttpActionResult res = BadRequest();

            if (serviceRes.Access == ResponseAccess.Granted)
                res = Ok(serviceRes.Page);
            if (serviceRes.Access == ResponseAccess.Deny)
                res = Unauthorized(serviceRes.Message);

            return res;
        }

        [WebApiCmsAuthorize(Premissions = PagePremision.CanManage)]
        [HttpPut]
        public async Task<IHttpActionResult> Put([FromUri]Guid id,[FromBody]PageAddUpdateViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(this.GetModelStateErorrs());
            var serviceRes = await _pageService.Update(new PageUpdateRequest()
            {
                RequestOwner = User,
                ViewModel = model,
                PageId = id
            });

            IHttpActionResult res = BadRequest();

            if (serviceRes.Access == ResponseAccess.Granted)
                res = Ok(serviceRes.Page);
            if (serviceRes.Access == ResponseAccess.Deny)
                res = Unauthorized(serviceRes.Message);

            return res;
        }
        [WebApiCmsAuthorize(Premissions = PagePremision.CanManage)]
        [HttpDelete]
        public async Task<IHttpActionResult> Delete(Guid id)
        {
            var serviceRes = await _pageService.Delete(new PageDeleteRequest()
            {
                RequestOwner = User,
                PageId = id
            });
            IHttpActionResult res = BadRequest();

            if (serviceRes.Access == ResponseAccess.Granted)
                res = Ok();
            if (serviceRes.Access == ResponseAccess.Deny)
                res = Unauthorized(serviceRes.Message);

            return res;
        }
    }
}
