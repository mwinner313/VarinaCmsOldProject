using System;
using System.Threading.Tasks;
using System.Web.Http;
using VarinaCmsV2.Common.WebApi;
using VarinaCmsV2.Core.Security.Premissions;
using VarinaCmsV2.Core.Services;
using VarinaCmsV2.Core.Services.Product;
using VarinaCmsV2.Core.Web.Security.WebApi;
using VarinaCmsV2.ViewModel.Eshop;

namespace VarinaCmsV2.Web.Areas.Admin.Controllers.Api
{
    [WebApiCmsAuthorize(Premissions = PanelPremission.Default)]
    public class ProductController : BaseApiController
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [WebApiCmsAuthorize(Premissions = ProductPremission.CanSee)]
        public async Task<IHttpActionResult> Get([FromUri]ProductQuery query)
        {
            var serviceRes = await _productService.Get(new ProductListRequest()
            {
                RequestOwner = User,
                Query = query ?? new ProductQuery()
            });

            IHttpActionResult res = BadRequest();

            if (serviceRes.Access == ResponseAccess.Granted)
                res = Ok(new { items = serviceRes.Products, count = serviceRes.Count });
            if (serviceRes.Access == ResponseAccess.Deny)
                res = Unauthorized(serviceRes.Message);

            return res;
        }
        [HttpGet]
        [WebApiCmsAuthorize(Premissions = ProductPremission.CanSee)]
        public async Task<IHttpActionResult> Get(Guid id)
        {
            var serviceRes = await _productService.Get(new ProductRequest()
            {
                RequestOwner = User,
                ProductId = id
            });

            IHttpActionResult res = BadRequest();

            if (serviceRes.Access == ResponseAccess.Granted)
                res = Ok(serviceRes.Product);
            if (serviceRes.Access == ResponseAccess.Deny)
                res = Unauthorized(serviceRes.Message);

            return res;
        }
        [HttpPost]
        [WebApiCmsAuthorize(Premissions = ProductPremission.CanAdd)]
        public async Task<IHttpActionResult> Post(ProductAddOrUpdateModel model)
        {
            var serviceRes = await _productService.Add(new ProductAddRequest()
            {
                RequestOwner = User,
                ProductToAdd = model
            });

            IHttpActionResult res = BadRequest();

            if (serviceRes.Access == ResponseAccess.Granted)
                res = Ok();
            if (serviceRes.Access == ResponseAccess.Deny)
                res = Unauthorized(serviceRes.Message);

            return res;
        }
        [HttpPut]
        [WebApiCmsAuthorize(Premissions = ProductPremission.CanUpdate)]
        public async Task<IHttpActionResult> Put(Guid id,ProductAddOrUpdateModel model)
        {
            var serviceRes = await _productService.Edit(new ProductEditRequest()
            {
                RequestOwner = User,
                Model = model,
                Id = id
            });

            IHttpActionResult res = BadRequest();

            if (serviceRes.Access == ResponseAccess.Granted)
                res = Ok();
            if (serviceRes.Access == ResponseAccess.Deny)
                res = Unauthorized(serviceRes.Message);

            return res;
        }

        [HttpDelete]
        [WebApiCmsAuthorize(Premissions = ProductPremission.CanDelete)]
        public async Task<IHttpActionResult> Delete(Guid id)
        {
            var serviceRes = await _productService.Delete(new ProductDeleteRequest()
            {
                RequestOwner = User,
                Id = id
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
