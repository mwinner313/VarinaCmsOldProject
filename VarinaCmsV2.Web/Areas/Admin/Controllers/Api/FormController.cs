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
using VarinaCmsV2.Core.Services.Form;
using VarinaCmsV2.Core.Services.FormScheme;
using VarinaCmsV2.Core.Web.Security.WebApi;

namespace VarinaCmsV2.Web.Areas.Admin.Controllers.Api
{
    [WebApiCmsAuthorize(Premissions = FormPremission.CanSee)]
    public class FormController : BaseApiController
    {
        private readonly IFormService _formService;
        public FormController(IFormService formService)
        {
            _formService = formService;
        }

        public async Task<IHttpActionResult> Get([FromUri]FormQuery query)
        {
            var serviceRes = await _formService.Get(new FormListRequest()
            {
                RequestOwner = User,
                Query = query
            });

            IHttpActionResult res = BadRequest();

            if (serviceRes.Access == ResponseAccess.Granted)
                res = Ok(new { items = serviceRes.Models, count = serviceRes.Count });
            if (serviceRes.Access == ResponseAccess.Deny)
                res = Unauthorized(serviceRes.Message);

            return res;
        }
        [HttpPatch]
        public async Task<IHttpActionResult> Patch(Guid id)
        {
            var serviceRes = await _formService.ChangeIsSeenState(new FormChangeIsSeenStateRequest()
            {
                RequestOwner = User,
                FormId = id
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
