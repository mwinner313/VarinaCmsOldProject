using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using VarinaCmsV2.Common.WebApi;
using VarinaCmsV2.Core.LogicFacade;
using VarinaCmsV2.Core.Security;
using VarinaCmsV2.Core.Security.Premissions;
using VarinaCmsV2.Core.Services;
using VarinaCmsV2.Core.Services.EntityScheme;
using VarinaCmsV2.Core.Web.ActionFilters;
using VarinaCmsV2.Core.Web.Security.WebApi;
using VarinaCmsV2.ViewModel.Entities;

namespace VarinaCmsV2.Web.Areas.Admin.Controllers.Api
{
    [WebApiCmsAuthorize(Roles = PreDefRoles.Developer)]
    [WebApiEnableEntityValidation]
    public class EntitySchemeController : BaseApiController
    {
        private readonly IEntitySchemeService _schemeService;
        public EntitySchemeController(IEntitySchemeService schemeService)
        {
            _schemeService = schemeService;
        }
        [WebApiCmsAuthorize(Roles = PreDefRoles.Developer, Premissions = SchemePremission.CanSee)]
        [Route("api/entityscheme/{handle}")]
        public async Task<IHttpActionResult> Get(string handle)
        {
            var res = await _schemeService.GetByHandleAsync(new EntitySchemeGetRequest() { Handle = handle, RequestOwner = User });
            if (res.Access == ResponseAccess.Granted)
                return Ok(res.EntityScheme);
            if (res.Access == ResponseAccess.Deny)
                return Unauthorized();

            return BadRequest();
        }
        [WebApiCmsAuthorize(Roles = PreDefRoles.Developer, Premissions = SchemePremission.CanSee)]
        public async Task<IHttpActionResult> Get(Guid id)
        {
            var res = await _schemeService.GetByIdAsync(new EntitySchemeGetRequest() { Id = id, RequestOwner = User });
            if (res.Access == ResponseAccess.Granted)
                return Ok(res.EntityScheme);
            if (res.Access == ResponseAccess.Deny)
                return Unauthorized();

            return BadRequest();
        }
        [WebApiCmsAuthorize(Roles = PreDefRoles.Developer, Premissions = SchemePremission.CanSee)]
        public async Task<IHttpActionResult> Get([FromUri]EntitySchemeQuery query)
        {
            var res = _schemeService.GetAsync(new EntitySchemeListRequest() { RequestOwner = User, Query = query });
            if (res.Access == ResponseAccess.Granted)
                return Ok(res.EntitySchemes);
            if (res.Access == ResponseAccess.Deny)
                return Unauthorized();
            return BadRequest();
        }
        [HttpPost]
        [WebApiCmsAuthorize(Roles = PreDefRoles.Developer, Premissions = SchemePremission.CanManage)]
        public async Task<IHttpActionResult> Post(EntitySchemeViewModel model)
        {
            var res = await _schemeService.AddAsync(new EntitySchemeAddRequest()
            {
                ViewModel = model,
                RequestOwner = User
            });

            if (res.Access == ResponseAccess.Granted)
                return Ok();
            if (res.Access == ResponseAccess.Deny)
                return Unauthorized();

            return BadRequest();
        }
        [HttpDelete]
        [Route("api/entityscheme/{id:guid}")]
        [WebApiCmsAuthorize(Roles = PreDefRoles.Developer, Premissions = SchemePremission.CanManage)]
        public async Task<IHttpActionResult> Delete(Guid id)
        {
            var res = await _schemeService.DeleteAsync(new EntitySchemeDeleteRequest()
            {
                Id = id,
                RequestOwner = User
            });

            if (res.Access == ResponseAccess.Granted)
                return Ok();
            if (res.Access == ResponseAccess.Deny)
                return Unauthorized();

            return BadRequest(res.Message);
        }
        [HttpPut]
        [Route("api/entityscheme/{id:guid}")]
        [WebApiCmsAuthorize(Roles = PreDefRoles.Developer, Premissions = SchemePremission.CanManage)]
        public async Task<IHttpActionResult> Put(Guid id,EntitySchemeUpdateViewModel model)
        {
            var res = await _schemeService.UpdateAsync(new EntitySchemeUpdateRequest()
            {
                ViewModel = model,
                RequestOwner = User
            });

            if (res.Access == ResponseAccess.Granted)
                return Ok();
            if (res.Access == ResponseAccess.Deny)
                return Unauthorized();

            return BadRequest();
        }
    }
}
