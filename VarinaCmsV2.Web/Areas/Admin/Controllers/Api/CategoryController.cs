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
using VarinaCmsV2.Core.Services.Category;
using VarinaCmsV2.Core.Web.Security.WebApi;
using VarinaCmsV2.ViewModel;
using VarinaCmsV2.ViewModel.Category;

namespace VarinaCmsV2.Web.Areas.Admin.Controllers.Api
{
    [WebApiCmsAuthorize(Premissions = PanelPremission.Default)]
    public class CategoryController : BaseApiController
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [WebApiCmsAuthorize(Premissions = CategoryPremission.CanSee)]
        public async Task<IHttpActionResult> Get([FromUri]CategoryQuery query)
        {
            IHttpActionResult res = BadRequest();

            var serviceRes = await _categoryService.Get(new CategoryListRequest()
            {
                Query = query,
                RequestOwner = User
            });
            if (serviceRes.Access == ResponseAccess.Deny) res = Unauthorized(serviceRes.Message);
            if (serviceRes.Access == ResponseAccess.Granted) res = Ok(serviceRes.Categories);
            return res;
        }

        [WebApiCmsAuthorize(Premissions = CategoryPremission.CanAdd)]
        public async Task<IHttpActionResult> Post(CategoryAddOrUpdateViewModel model)
        {
            IHttpActionResult res = BadRequest();
            var serviceRes = await _categoryService.Add(new CategoryAddRequest()
            {
                RequestOwner = User,
                Model = model
            });
            if (serviceRes.Access == ResponseAccess.Granted) res = Ok(serviceRes.Category);
            if (serviceRes.Access == ResponseAccess.Deny) res = Unauthorized(serviceRes.Message);
            return res;
        }
        [WebApiCmsAuthorize(Premissions = CategoryPremission.CanEdit)]
        [HttpPut]

        public async Task<IHttpActionResult> Put([FromUri]Guid id, CategoryAddOrUpdateViewModel model)
        {
            IHttpActionResult res = BadRequest();
            if (id != model.Id) return res;

            var serviceRes = await _categoryService.Update(new CategoryUpdateRequest()
            {
                RequestOwner = User,
                ViewModel = model,
                Id = id
            });
            if (serviceRes.Access == ResponseAccess.Granted) res = Ok(serviceRes.Category);
            if (serviceRes.Access == ResponseAccess.Deny) res = Unauthorized(serviceRes.Message);
            return res;
        }

        [WebApiCmsAuthorize(Premissions = CategoryPremission.CanDelete)]
        [HttpDelete]

        public async Task<IHttpActionResult> Delete(Guid id)
        {
            IHttpActionResult res = BadRequest();
            var serviceRes = await _categoryService.Delete(new CategoryDeleteRequest()
            {
                RequestOwner = User,
                Id = id
            });
            if (serviceRes.Access == ResponseAccess.Granted) res = Ok();
            if (serviceRes.Access == ResponseAccess.Deny) res = Unauthorized(serviceRes.Message);
            if (serviceRes.Access == ResponseAccess.BadRequest) res = BadRequest(serviceRes.Message);
            return res;
        }

        [WebApiCmsAuthorize(Premissions = CategoryPremission.CanEdit)]
        [Route("api/category/UpdateChildParent/{schemeId:guid}")]
        [HttpPut]
        public async Task<IHttpActionResult> UpdateChildParent(Guid schemeId, List<ParentChildIdViewModel> model)
        {
            IHttpActionResult res = BadRequest();
            var serviceRes = await _categoryService.UpdateParentListRequest(new CategoryParentChildUpdateRequest()
            {
                RequestOwner = User,
                ViewModel = model,
                SchemeId = schemeId
            });

            if (serviceRes.Access == ResponseAccess.Granted) res = Ok();
            if (serviceRes.Access == ResponseAccess.Deny) res = Unauthorized();
            return res;
        }
    }
}
