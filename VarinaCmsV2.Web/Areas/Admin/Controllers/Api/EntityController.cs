using System;
using System.Threading.Tasks;
using System.Web.Http;
using VarinaCmsV2.Common.WebApi;
using VarinaCmsV2.Core.LogicFacade;
using VarinaCmsV2.Core.Security.Premissions;
using VarinaCmsV2.Core.Services;
using VarinaCmsV2.Core.Services.Entity;
using VarinaCmsV2.Core.Web.ActionFilters;
using VarinaCmsV2.Core.Web.Security.WebApi;
using VarinaCmsV2.ViewModel.Entities;

namespace VarinaCmsV2.Web.Areas.Admin.Controllers.Api
{
    [WebApiCmsAuthorize(Premissions = PanelPremission.Default)]
    public class EntityController : BaseApiController
    {
        private readonly IEntityService _entityService;

        public EntityController(IEntityFacade entityFacade, IEntityService entityService)
        {
            _entityService = entityService;
        }
        [WebApiCmsAuthorize(Premissions = EntityPremission.CanSee)]
        public async Task<IHttpActionResult>Get([FromUri]EntityQuery query)
        {
            IHttpActionResult res = BadRequest();
            var serviceRes = await _entityService.Get(new EntityListRequest()
            {
                Query = query??new EntityQuery(),
                RequestOwner = this.User
            });

            if (serviceRes.Access == ResponseAccess.Deny)
                res = Unauthorized(serviceRes.Message);

            if (serviceRes.Access == ResponseAccess.Granted)
                res = Ok(serviceRes);

            return res;
        }
        [WebApiCmsAuthorize(Premissions = EntityPremission.CanSee)]

        [Route("api/entity/{schemeHandle}/{id:guid}")]
        public async Task<IHttpActionResult> Get(string schemeHandle, Guid id)
        {
            IHttpActionResult res = BadRequest();

            var serviceRes = await _entityService.Get(new EntityGetRequest()
            {
                Id = id,
                SchemeHandle = schemeHandle,
                RequestOwner = User
            });

            if (serviceRes.Access == ResponseAccess.Granted)
                res = Ok(serviceRes.Entity);

            if (serviceRes.Access == ResponseAccess.Deny)
                res = Unauthorized(serviceRes.Message);

            return res;
        }

        [HttpPost]
        [WebApiCmsAuthorize(Premissions = EntityPremission.CanAdd)]
        [WebApiEnableEntityValidation]
        public async Task<IHttpActionResult> Post(EntityAddOrUpdateViewModel model)
        {
            IHttpActionResult res = BadRequest();
            if (!ModelState.IsValid) return res;

            var serviceRes = await _entityService.Add(new EntityAddRequest()
            {
                RequestOwner = User,
                ViewModel = model
            });

            if (serviceRes.Access == ResponseAccess.Granted)
                res = Ok(model);

            if (serviceRes.Access == ResponseAccess.Deny)
                res = Unauthorized(serviceRes.Message);

            return res;
        }
        [HttpPut]
        [WebApiCmsAuthorize(Premissions = EntityPremission.CanEdit)]
        [WebApiEnableEntityValidation]
        [Route("api/entity/{schemeHandle}/{id:guid}")]
        public async Task< IHttpActionResult >Put(string schemeHandle, Guid id, EntityAddOrUpdateViewModel model)
        {
            IHttpActionResult res = BadRequest();
            if (!ModelState.IsValid)
                return
                    base.BadRequest(GetModelStateErorrs());

            if (id != model.Id) return res;

            var serviceRes = await _entityService.Update(new EntityUpdateRequest()
            {
                RequestOwner = User,
                ViewModel = model,
                EntityId = id,
                SchemeHandle = schemeHandle
            });

            if (  serviceRes.Access == ResponseAccess.Granted)
                res = Ok(model);

            if (serviceRes.Access == ResponseAccess.Deny)
                res = Unauthorized(serviceRes.Message);

            return res;
        }

       

        [HttpDelete]
        [Route("api/entity/{schemeHandle}/{id:guid}")]
        [WebApiCmsAuthorize(Premissions = EntityPremission.CanDelete)]
        [WebApiEnableEntityValidation]
        public async Task<IHttpActionResult >Delete(string schemeHandle, Guid id)
        {
            IHttpActionResult res = BadRequest();
            var serviceRes = await _entityService.Delete(new EntityDeleteRequest()
            {
                SchemeHandle = schemeHandle,
                RequestOwner = User,
                EntityId = id
            });

            if (serviceRes.Access == ResponseAccess.Granted)
                res = Ok();

            if (serviceRes.Access == ResponseAccess.Deny)
                res = Unauthorized(serviceRes.Message);

            return res;
        }

    }
}
