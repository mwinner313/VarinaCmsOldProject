using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using NetTelebot.Type;
using VarinaCmsV2.Core.Contracts.Security;
using VarinaCmsV2.Core.Contracts.Web;
using VarinaCmsV2.Core.Contracts.WebClientServices.Products;
using VarinaCmsV2.Core.Logic.Helpers;
using VarinaCmsV2.Core.Services;
using VarinaCmsV2.Core.Web;
using VarinaCmsV2.Core.Web.Security.Mvc;
using VarinaCmsV2.DomainClasses.Entities.EShop;
using VarinaCmsV2.ViewModel.Eshop.Review;

namespace VarinaCmsV2.Web.Controllers
{
    [AllowAnonymous]
    public class ProductController : LiquidController
    {
        private readonly IProductWebClientService _productWebClientService;
        private readonly ICmsUrlBuilderFactory _cmsUrlBuilder;
        public ProductController(IProductWebClientService productWebClientService, IRestrictedItemAccessManager accessManager)
        {
            _productWebClientService = productWebClientService;
        }
        public async Task<ActionResult> ShowItemAsync(string productUrl)
        {
            var product = await _productWebClientService
                
                .GetByUrlAsync(productUrl);

            if (product == null)
                return LiquidNotFoundView();
            return LiquidView(product.AsLiquidAdapted());
        }

        public async Task<ActionResult> SearchAsync(string q)
        {
            var res = await _productWebClientService
                
                .Search(q);

            if (Request.IsAjaxRequest()) return Json(res);

            return LiquidView(res.MapToLiquidView(), "search-products");
        }
        public async Task<ActionResult> CompareAsync(string product1, string product2, string product3, string product4)
        {
            var res = await _productWebClientService
                
                .GetProductCompareViewData(product1, product2, product3, product4);

            return LiquidView(res.MapToLiquidViewModel(), "compare-products");
        }
        public async Task<ActionResult> ListByCategoryAsync(string productCategoryUrl, int pageNumber)
        {
            var paginatedByCategory = await _productWebClientService
                
                .ListByCategoryAsync(productCategoryUrl, pageNumber);

            return LiquidView(paginatedByCategory.AsLiquidAdapted());
        }
        public async Task<ActionResult> ListByTagAsync(string tagUrl, int pageNumber)
        {
            var paginatedByCategory = await _productWebClientService
                
                .ListByTagAsync(tagUrl, pageNumber);

            return LiquidView(paginatedByCategory.AsLiquidAdapted());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [MvcCmsAuthorize]
        public async Task<ActionResult> AddCustomerReview(ReviewAddOrUpdateViewModel model)
        {
            var res = await _productWebClientService
                
                .AddCustomerReview(new AddCostomerReviewRequest()
            {
                Model = model,
                RequestOwner = User
            });

            if (Request.IsAjaxRequest())
                return Json(res);


            if (res.Access == ResponseAccess.Deny) return new HttpUnauthorizedResult();

            return Redirect(_cmsUrlBuilder.GetUrlBuilder(res.Product).Generate(res.Product));
        }
        [HttpGet]
        [MvcCmsAuthorize]
        [CountUpOrderItemDownloadCountAfterDownloadCompeletedByUser]
        public async Task<ActionResult> DownloadAsync(Guid orderId, Guid productId)

        {
            var res = await _productWebClientService.GetProductForDownLoad(orderId, productId);

            switch (res.Status)
            {
                case DownloadResponseStatus.DownLoadAllowed:
                    return File(res.Product.File.Path, MimeMapping.GetMimeMapping(res.Product.File.Path));
                case DownloadResponseStatus.FileNotExists: return HttpNotFound();
                //todo
                case DownloadResponseStatus.DownLoadNotAllowedDueToDownLoadCountRestriction:
                case DownloadResponseStatus.DownLoadNotAllowedDueToExpirationDate:
                case DownloadResponseStatus.DownLoadNotAllowedDueToUserNotPurchasedTheItemInSpecifiedOrder:
                default: return new HttpUnauthorizedResult("forebidden 403");
            }
        }
        [HttpGet]
        [MvcCmsAuthorize]
        [CountUpOrderItemDownloadCountAfterDownloadCompeletedByUser]
        public async Task<ActionResult> DownloadAsync(Guid productId)
        {
            var res = await _productWebClientService.GetProductSampleForDownLoad(productId);

            if (res.SampleFile == null)
                return HttpNotFound();

            return File(res.SampleFile.Path, MimeMapping.GetMimeMapping(res.SampleFile.Path));
        }
    }
}