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
using VarinaCmsV2.Core.Services.FormScheme;
using VarinaCmsV2.Core.Web.Security.WebApi;
using VarinaCmsV2.DomainClasses.Entities;
using VarinaCmsV2.ViewModel.Forms;

namespace VarinaCmsV2.Web.Areas.Admin.Controllers.Api
{
    [WebApiCmsAuthorize(Premissions = FormSchemePremission.CanSee)]
    public class FormSchemeController : BaseApiController
    {
        private readonly IFormSchemeService _formSchemeService;
        public FormSchemeController(IFormSchemeService formSchemeService)
        {
            _formSchemeService = formSchemeService;
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get([FromUri]FormSchemeQuery query)
        {
            var serviceRes = await _formSchemeService.Get(new FormSchemeGetListRequest()
            {
                Query = query,
                RequestOwner = User,

            });

            IHttpActionResult res = BadRequest();

            if (serviceRes.Access == ResponseAccess.Granted)
                res = Ok(new { items = serviceRes.Models, count = serviceRes.Count });
            if (serviceRes.Access == ResponseAccess.Deny)
                res = Unauthorized(serviceRes.Message);

            return res;
        }
        [Route("api/formScheme/{formSchemeHandle}")]
        [HttpGet]
        public async Task<IHttpActionResult> Get(string formSchemeHandle)
        {
            var serviceRes = await _formSchemeService.Get(new FormSchemeGetRequest()
            {
                RequestOwner = User,FormSchemeHandle = formSchemeHandle
            });

            IHttpActionResult res = BadRequest();

            if (serviceRes.Access == ResponseAccess.Granted)
                res = Ok(serviceRes.Model);
            if (serviceRes.Access == ResponseAccess.Deny)
                res = Unauthorized(serviceRes.Message);

            return res;
        }
        [WebApiCmsAuthorize(Premissions = FormSchemePremission.CanAdd)]
        [HttpPost]
        public async Task<IHttpActionResult> Post(FormSchemeAddOrUpdateViewModel model)
        {
            var serviceRes = await _formSchemeService.Add(new FormSchemeAddReqest()
            {
                Model = model,
                RequestOwner = User,
            });

            IHttpActionResult res = BadRequest();

            if (serviceRes.Access == ResponseAccess.Granted)
                res = Ok(serviceRes.Model);
            if (serviceRes.Access == ResponseAccess.Deny)
                res = Unauthorized(serviceRes.Message);

            return res;
        }
        [WebApiCmsAuthorize(Roles = PreDefRoles.Developer, Premissions = FormSchemePremission.CanUpdate)]
        [HttpPut]
        [Route("api/formScheme/{id}")]
        public async Task<IHttpActionResult> Put(Guid id, [FromBody]FormSchemeAddOrUpdateViewModel model)
        {
            var serviceRes = await _formSchemeService.Edit(new FormSchemeEditRequest()
            {
                Model = model,
                RequestOwner = User,
                FormSchemeId = id
            });

            IHttpActionResult res = BadRequest();

            if (serviceRes.Access == ResponseAccess.Granted)
                res = Ok(serviceRes.Model);
            if (serviceRes.Access == ResponseAccess.Deny)
                res = Unauthorized(serviceRes.Message);

            return res;
        }

        [WebApiCmsAuthorize(Premissions = FormSchemePremission.CanDelete)]
        [Route("api/formScheme/{id}")]
        [HttpDelete]
        public async Task<IHttpActionResult> Delete(Guid id)
        {
            var serviceRes = await _formSchemeService.Delete(new FormSchemeDeleteRequest()
            {
                FormSchemeId = id,
                RequestOwner = User,
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
