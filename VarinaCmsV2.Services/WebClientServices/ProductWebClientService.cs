using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNet.Identity;
using VarinaCmsV2.Common;
using VarinaCmsV2.Core.Contracts;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.Core.Contracts.Security;
using VarinaCmsV2.Core.Contracts.WebClientServices.Discounts;
using VarinaCmsV2.Core.Contracts.WebClientServices.Products;
using VarinaCmsV2.Core.FrontEndOptions;
using VarinaCmsV2.Core.Services;
using VarinaCmsV2.Core.Services.Settings;
using VarinaCmsV2.Core.Settings;
using VarinaCmsV2.Data.Contracts;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.DomainClasses.Entities.Enums;
using VarinaCmsV2.DomainClasses.Entities.EShop;
using VarinaCmsV2.DomainClasses.Entities.EShop.Orders;
using VarinaCmsV2.Security.Contracts;
using VarinaCmsV2.ViewModel.Eshop;
using VarinaCmsV2.ViewModel.Eshop.Review;

namespace VarinaCmsV2.Services.WebClientServices
{
    public class ProductWebClientService : IProductWebClientService
    {
        private readonly IRestrictedItemAccessManager _accessManager;
        private readonly Lazy<Guid> _currUserId;
        private readonly IDiscountWebClientService _discountWebClientService;
        private readonly IIdentityManager _identityManager;
        private readonly IOrderDataService _orderDataService;
        private readonly int _pageSize = FrontEndDeveloperOptions.Instance.Pagination.Default;
        private readonly IProductCategoryDataService _productCategoryDataService;
        private readonly IProductDataService _productDataService;
        private readonly IProductReviewDataService _productReviewDataService;
        private readonly IEntitySchemeDataService _schemeDataService;
        private readonly ISecurityLogger _securityLogger;
        private readonly ISettingService _settingService;
        private readonly ITagDataService _tagDataService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWorkContext _workContext;

        public ProductWebClientService(IProductDataService productDataService,
            IRestrictedItemAccessManager accessManager, IProductCategoryDataService productCategoryDataService,
            IWorkContext workContext, IUnitOfWork unitOfWork, IProductReviewDataService productReviewDataService,
            ISecurityLogger securityLogger, IOrderDataService orderDataService, ShoppingCartHelper shoppingCartHelper,
            IDiscountWebClientService discountWebClientService, ITagDataService tagDataService,
            ISettingService settingService, IIdentityManager identityManager,
            IEntitySchemeDataService schemeDataService)
        {
            _productDataService = productDataService;
            _accessManager = accessManager;
            _productCategoryDataService = productCategoryDataService;
            _workContext = workContext;
            _unitOfWork = unitOfWork;
            _productReviewDataService = productReviewDataService;
            _securityLogger = securityLogger;
            _orderDataService = orderDataService;
            _discountWebClientService = discountWebClientService;
            _tagDataService = tagDataService;
            _settingService = settingService;
            _identityManager = identityManager;
            _schemeDataService = schemeDataService;
            _currUserId = new Lazy<Guid>(_identityManager.GetCurrentIdentity().GetUserId().ToGuid);
        }

        public async Task<Product> GetByUrlAsync(string productUrl)
        {
            var product = await _productDataService.Query.Include(x => x.PrimaryCategory)
                .Include(x => x.Eshantions)
                .Include(x => x.RequiredProducts)
                .Include(x => x.UpSellings)
                .Include(x => x.DeliveryDates)
                .Include(x => x.Pictures)
                .Include(x => x.ProductReviews)
                .Include(x => x.AssociatedProducts)
                .Include(x => x.SampleFile)
                .Include(x => x.Fields)
                .Include(x => x.RelatedCategories)
                .Include(x => x.Tags)
                .Include(x => x.Fields.Select(f => f.FieldDefenition)).FirstOrDefaultAsync(x => x.Url == productUrl);

            product.AppliedDiscounts = _discountWebClientService.GetProductAvilableDiscounts(product);

            if (!_accessManager.HasAccess(product, AccessPremission.See)) return null;

            if (!product.VisibleIndividually && product.Type == ProductType.Simple) return null;

            return product;
        }

        public async Task<PaginatedProductCategory> ListByCategoryAsync(string productCategoryUrl, int pageNumber)
        {
            var category = await _productCategoryDataService.Query.FirstAsync(x => x.Url == productCategoryUrl);
            var query = _accessManager
                .Filter(_productDataService.Query.Where(x => x.PrimaryCategoryId == category.Id))
                .OrderByDescending(x => x.PublishDateTime);

            var retValue = new PaginatedProductCategory
            {
                AllPagesCount = query.GetAllPagesCount(FrontEndDeveloperOptions.Instance.Pagination.Default),
                CurrentPage = pageNumber,
                ProductCategory = category,
                Products = await query
                    .Paginate(_pageSize, pageNumber)
                    .ToListAsync()
            };
            retValue.Products.ForEach(
                FetchAvalailbleDiscounts);
            return retValue;
        }

        public async Task<PaginatedProducTag> ListByTagAsync(string tagUrl, int pageNumber)
        {
            var tag = await _tagDataService.Query.FirstAsync(x => x.Url == tagUrl);
            var query = _accessManager
                .Filter(_productDataService.Query.Where(x => x.Tags.Any(t => t.Id == tag.Id)))
                .OrderByDescending(x => x.PublishDateTime);

            var retValue = new PaginatedProducTag
            {
                AllPagesCount = query.GetAllPagesCount(FrontEndDeveloperOptions.Instance.Pagination.Default),
                CurrentPage = pageNumber,
                Tag = tag,
                Products = await query
                    .Paginate(_pageSize, pageNumber)
                    .ToListAsync()
            };
            retValue.Products.ForEach(
                FetchAvalailbleDiscounts);
            return retValue;
        }

        public async Task<ProductsCompareModel> GetProductCompareViewData(params string[] products)
        {
            if (products is null || products.Length == 0) return null;

            var compareProducts = await _accessManager.Filter(_productDataService.Query)
                .Where(x => products.Contains(x.Url))
                .Include(x => x.Fields)
                .Include(x => x.Pictures)
                .ToListAsync();

            if (!AllProductHasSameSchemeAndCat(compareProducts)) return null;

            var retVal = new ProductsCompareModel
            {
                Products = compareProducts,
                Scheme = await _schemeDataService.GetAsync(compareProducts.First().SchemeId),
                PrimaryCategory = await _productCategoryDataService.GetAsync(compareProducts.First().PrimaryCategoryId)
            };
            retVal.Products.ForEach(FetchAvalailbleDiscounts);

            return retVal;
        }

        public async Task<DownloadProductResponse> GetProductForDownLoad(Guid orderId, Guid productId)
        {
            var product = _productDataService.Query.Include(x => x.File)
                .FirstOrDefault(x => x.Id == productId && x.IsDownLoadFile);

            if (product is null)
                return new DownloadProductResponse {Status = DownloadResponseStatus.FileNotExists};

            var order = _orderDataService.Query.Include(x => x.OrderItems).FirstOrDefault(x =>
                x.Id == orderId
                && x.PaymentStatus == PaymentStatus.Paid
                && x.CreatorId == _currUserId.Value);

            if (order is null)
                return new DownloadProductResponse
                {
                    Status = DownloadResponseStatus.DownLoadNotAllowedDueToOrderNotExists
                };

            var orderItem = order.OrderItems.FirstOrDefault(x => x.ProductId == productId);

            if (orderItem is null)
                return new DownloadProductResponse
                {
                    Status = DownloadResponseStatus.DownLoadNotAllowedDueToUserNotPurchasedTheItemInSpecifiedOrder
                };


            if (!product.UnlimitedDownloads && product.MaxNumberOfDownloads <= orderItem.DownloadCount)
                return new DownloadProductResponse
                {
                    Status = DownloadResponseStatus.DownLoadNotAllowedDueToDownLoadCountRestriction
                };

            if (product.DownloadExpirationDays.HasValue)
                switch (product.DownLoadActivationType)
                {
                    case ProductDownLoadActivationType.WhenPaid:
                        if (order.PaidDate > DateTime.Now.AddDays(product.DownloadExpirationDays.Value))
                            return new DownloadProductResponse
                            {
                                Status = DownloadResponseStatus.DownLoadNotAllowedDueToExpirationDate
                            };
                        break;
                    case ProductDownLoadActivationType.Manually:
                        if (!orderItem.DownloadExpirationDate.HasValue ||
                            orderItem.DownloadExpirationDate < DateTime.Now)
                            return new DownloadProductResponse
                            {
                                Status = DownloadResponseStatus.DownLoadNotAllowedDueToExpirationDate
                            };
                        break;
                }

            return new DownloadProductResponse
            {
                Product = product,
                Status = DownloadResponseStatus.DownLoadAllowed
            };
        }

        public async Task<DownloadSampleProductResponse> GetProductSampleForDownLoad(Guid productId)
        {
            var product = await _productDataService.Query.Include(x => x.SampleFile)
                .FirstOrDefaultAsync(x => x.Id == productId);
            if (!_accessManager.HasAccess(product, AccessPremission.See))
            {
                _securityLogger.LogDangeriousActionAttemp(_identityManager.GetCurrentPrincipal(), product,
                    "download a sample file of unAuthoriezed product to see ");
                return new DownloadSampleProductResponse();
            }

            if (product is null || !product.HasSampleFile)
                return new DownloadSampleProductResponse();

            return new DownloadSampleProductResponse {SampleFile = product.SampleFile};
        }

        public async Task<SearchProductResult> Search(string searchText)
        {
            if (string.IsNullOrEmpty(searchText)) return new SearchProductResult();

            var products = _accessManager.Filter(_productDataService.Query.Include(x => x.Pictures).Where(x =>
                x.Title.Contains(searchText) ||
                x.Tags.Any(t => t.Title.Contains(searchText)) ||
                x.PrimaryCategory.Title.Contains(searchText))).OrderByDescending(x => x.PublishDateTime);

            var retVal = new SearchProductResult
            {
                Count = await products.LongCountAsync(),
                Products = await products.ToListAsync(),
                SearchText = searchText
            };

            retVal.Products.ForEach(
                FetchAvalailbleDiscounts);

            return retVal;
        }
        public async Task<AddCustomerReviewResponse> AddCustomerReview(AddCostomerReviewRequest model)
        {
            var sitePolicies = _settingService.GetSetting<WebSitePolicies>();
            var product = _productDataService.Query.First(x => x.Id == model.Model.ProductId);
            if (!_accessManager.HasAccess(product, AccessPremission.See))
            {
                _securityLogger.LogDangeriousAddAttemp(model.RequestOwner, model.Model);
                return new AddCustomerReviewResponse {Access = ResponseAccess.Deny};
            }

            if (!product.AllowCustomerReviews)
                return new AddCustomerReviewResponse
                {
                    Access = ResponseAccess.Deny,
                    Message = "err"
                };

            var doesBied = DoesUserBuiedAndOrderCompeleted(product);

            if (sitePolicies.EShopReviewPolicy == EShopReviewPolicy.BuiedUsers && !doesBied)
                return new AddCustomerReviewResponse
                {
                    Access = ResponseAccess.Deny,
                    Message = "err"
                };

            if (sitePolicies.EShopReviewPolicy == EShopReviewPolicy.BuiedUsersInRoles && !doesBied ||
                !_identityManager.CurrentIdentityHasOneOfRoles(
                    sitePolicies.EShopReviewPolicyUserInRoles.Select(x => x.Name)))
                return new AddCustomerReviewResponse
                {
                    Access = ResponseAccess.Deny,
                    Message = "err"
                };

            if (sitePolicies.EShopReviewPolicy == EShopReviewPolicy.UsersInRoles &&
                !_identityManager.CurrentIdentityHasOneOfRoles(
                    sitePolicies.EShopReviewPolicyUserInRoles.Select(x => x.Name)))
                return new AddCustomerReviewResponse
                {
                    Access = ResponseAccess.Deny,
                    Message = "err"
                };
            if (sitePolicies.EShopReviewPolicy == EShopReviewPolicy.UsersInRoles &&
                !_identityManager.CurrentIdentityHasOneOfRoles(
                    sitePolicies.EShopReviewPolicyUserInRoles.Select(x => x.Name)))
                return new AddCustomerReviewResponse
                {
                    Access = ResponseAccess.Deny,
                    Message = "err"
                };


            model.Model.Id = Guid.Empty;
            var review = Mapper.Map<ProductReview>(model.Model);
            review.UserId = _workContext.CurrentUser.Id;
            review.IsApproved = false;
            product.ProductReviews.Add(review);
            await _unitOfWork.SaveChangesAsync();
            return new AddCustomerReviewResponse
            {
                Access = ResponseAccess.Granted,
                Review = Mapper.Map<ReviewViewModel>(review),
                Product = product
            };
        }

        private static bool AllProductHasSameSchemeAndCat(List<Product> compareProducts)
        {
            var schemeId = compareProducts.First().SchemeId;
            var catId = compareProducts.First().PrimaryCategoryId;

            return compareProducts.All(x =>
                x.SchemeId == schemeId || x.PrimaryCategoryId == catId);
        }

        private void FetchAvalailbleDiscounts(Product product)
        {
            product.AppliedDiscounts = _discountWebClientService.GetProductAvilableDiscounts(product);
        }

        private bool DoesUserBuiedAndOrderCompeleted(Product product)
        {
            return _orderDataService.Query.Any(x =>
                x.OrderItems.Any(i => i.ProductId == product.Id) && x.OrderStatus == OrderStatus.Complete &&
                x.CreatorId == _workContext.CurrentUser.Id);
        }
    }
}