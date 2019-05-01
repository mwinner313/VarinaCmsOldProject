using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Http;
using VarinaCmsV2.Common.WebApi;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.Core.Security.Premissions;
using VarinaCmsV2.Core.Web.Security.WebApi;
using VarinaCmsV2.Web.Areas.Admin.Models;

namespace VarinaCmsV2.Web.Areas.Admin.Controllers.Api.Validation
{
    [WebApiCmsAuthorize(Premissions = EntityPremission.CanSee)]

    public class EntityValidatorController : BaseApiController
    {
        private readonly IEntityDataService _entityDataService;

        public EntityValidatorController(IEntityDataService entityDataService)
        {
            _entityDataService = entityDataService;
        }


        public async Task<IHttpActionResult> ExistsTitle(EntityValidationContext model)
        {
            if (model.Id.HasValue && await _entityDataService.Query.AnyAsync(x =>
                    x.Title == model.Value && x.Id == model.Id && model.SchemeId == x.SchemeId))
                return Ok(new
                {
                    value = model,
                    isValid = true
                });

            return Ok(new
            {
                value = model,
                isValid = !await _entityDataService.Query.AnyAsync(x => x.Title== model.Value &&x.SchemeId==model.SchemeId)
            });
        }
    }
}