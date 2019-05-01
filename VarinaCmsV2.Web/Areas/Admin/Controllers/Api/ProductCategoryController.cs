using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using VarinaCmsV2.Common.WebApi;
using VarinaCmsV2.Core.Security.Premissions;
using VarinaCmsV2.Core.Services;
using VarinaCmsV2.Core.Services.Category;
using VarinaCmsV2.Core.Services.Product;
using VarinaCmsV2.Core.Services.ProductCategories;
using VarinaCmsV2.Core.Web.Security.WebApi;
using VarinaCmsV2.ViewModel;
using VarinaCmsV2.ViewModel.Eshop;

namespace VarinaCmsV2.Web.Areas.Admin.Controllers.Api
{
    public class ProductCategoryController : BaseApiController
    {
        private readonly IProductCategoryService _productCatService;

        public ProductCategoryController(IProductCategoryService productCatService)
        {
            _productCatService = productCatService;
        }

        [HttpGet]
        [WebApiCmsAuthorize(Premissions = ProductCategoryPremission.CanSee)]
        public async Task<IHttpActionResult> Get([FromUri]ProductCategoryQuery query)
        {
            var serviceRes = await _productCatService.Get(new ProductCategoryListRequest()
            {
                RequestOwner = User,
                Query = query ?? new ProductCategoryQuery()
            });

            IHttpActionResult res = BadRequest();

            if (serviceRes.Access == ResponseAccess.Granted)
                res = Ok(serviceRes.Categories);

            if (serviceRes.Access == ResponseAccess.Deny)
                res = Unauthorized(serviceRes.Message);

            return res;
        }

        [WebApiCmsAuthorize(Premissions = ProductCategoryPremission.CanAdd)]
        [HttpPost]
        public async Task<IHttpActionResult> Post(ProductCategoryAddOrUpdateModel model)
        {
            IHttpActionResult res = BadRequest();
            var serviceRes = await _productCatService.Add(new ProductCategoryAddRequest()
            {
                RequestOwner = User,
                Model = model,
            });

            if (serviceRes.Access == ResponseAccess.Granted) res = Ok();
            if (serviceRes.Access == ResponseAccess.Deny) res = Unauthorized();
            return res;
        }

        [WebApiCmsAuthorize(Premissions = ProductCategoryPremission.CanEdit)]
        [HttpPut]
        public async Task<IHttpActionResult> Put(Guid id, ProductCategoryAddOrUpdateModel model)
        {
            IHttpActionResult res = BadRequest();
            var serviceRes = await _productCatService.Edit(new ProductCategoryEditRequest()
            {
                RequestOwner = User,
                Model = model,
                Id = id
            });

            if (serviceRes.Access == ResponseAccess.Granted) res = Ok();
            if (serviceRes.Access == ResponseAccess.Deny) res = Unauthorized();
            return res;
        }

        [WebApiCmsAuthorize(Premissions = ProductCategoryPremission.CanDelete)]
        [HttpDelete]
        public async Task<IHttpActionResult> Delete(Guid id)
        {
            IHttpActionResult res = BadRequest();
            var serviceRes = await _productCatService.Delete(new ProductCategoryDeleteRequest()
            {
                RequestOwner = User,
                Id = id
            });

            if (serviceRes.Access == ResponseAccess.Granted) res = Ok();
            if (serviceRes.Access == ResponseAccess.Deny) res = Unauthorized();
            if (serviceRes.Access == ResponseAccess.BadRequest) res = BadRequest(serviceRes.Message);
            return res;
        }

        [WebApiCmsAuthorize(Premissions = ProductCategoryPremission.CanEdit)]
        [Route("api/productCategory/UpdateChildParent")]
        [HttpPut]
        public async Task<IHttpActionResult> UpdateChildParent(List<ParentChildIdViewModel> model)
        {
            IHttpActionResult res = BadRequest();
            var serviceRes = await _productCatService.UpdateParentListRequest(new ParentChildUpdateRequest()
            {
                RequestOwner = User,
                ViewModel = model,
            });

            if (serviceRes.Access == ResponseAccess.Granted) res = Ok();
            if (serviceRes.Access == ResponseAccess.Deny) res = Unauthorized();
            return res;
        }
    }
}
