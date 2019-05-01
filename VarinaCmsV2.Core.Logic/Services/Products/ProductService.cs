using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using VarinaCmsV2.Common;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.Core.Contracts.Security;
using VarinaCmsV2.Core.Infrastructure;
using VarinaCmsV2.Core.Logic.Helpers;
using VarinaCmsV2.Core.LogicFacade;
using VarinaCmsV2.Core.Services;
using VarinaCmsV2.Core.Services.Product;
using VarinaCmsV2.Data.Contracts;
using VarinaCmsV2.DomainClasses.Entities.Enums;
using VarinaCmsV2.DomainClasses.Entities.EShop;
using VarinaCmsV2.Security.Contracts;

namespace VarinaCmsV2.Core.Logic.Services.Products
{
    public class ProductService : BaseService<Product, ProductResponse, ProductListResponse>, IProductService
    {
        private readonly IProductDataService _productDataService;
        private readonly IProductFacade _productFacade;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductCategoryDataService _productCategoryDataService;
        private readonly IEntitySchemeDataService _schemeDataService;
        public ProductService(List<IBaseBeforeAddingEntityLogic> baseBeforeAddingEntityLogics,
            List<BaseAfterAddingEntityLogic> baseAfterAddingEntityLogics,
            List<IBaseAfterUpdatingEntityLogic> baseAfterUpdateEntityLogics,
            List<IBaseBeforeUpdatingEntityLogic> baseBeforeUpdateEntityLogics,
            List<IBaseBeforeDeleteEntityLogic> baseBeforeDeleteEntityLogics,
            List<BaseAfterDeleteEntityLogic> baseAfterDeleteEntityLogics, IIdentityManager identityManager,
            IRestrictedItemAccessManager accessManager,
            IProductDataService productDataService, IProductFacade productFacade, IUnitOfWork unitOfWork, IEntitySchemeDataService schemeDataService, IProductCategoryDataService productCategoryDataService) : base(baseBeforeAddingEntityLogics, baseAfterAddingEntityLogics,
            baseAfterUpdateEntityLogics, baseBeforeUpdateEntityLogics, baseBeforeDeleteEntityLogics,
            baseAfterDeleteEntityLogics, identityManager, accessManager, productDataService)
        {
            _productDataService = productDataService;
            _productFacade = productFacade;
            _unitOfWork = unitOfWork;
            _schemeDataService = schemeDataService;
            _productCategoryDataService = productCategoryDataService;
        }

        public async Task<ProductListResponse> Get(ProductListRequest request)
        {
            var productQuery = base.AccessManager.Filter(_productDataService.Query).AsNoTracking()
                .FilterByProductQuery(request.Query)
                .Include(x => x.Creator).Include(x => x.Pictures).Include(x => x.PrimaryCategory);

            return
                new ProductListResponse()
                {
                    Access = ResponseAccess.Granted,
                    Count = await productQuery.LongCountAsync(),
                    Products = (await productQuery.Paginate(request.Query).ToListAsync()).MapToViewModel()
                };

        }

        public async Task<ProductResponse> Get(ProductRequest request)
        {
            var product = await _productDataService.Query.IncludeNecessaryData()
                .FirstAsync(x => x.Id == request.ProductId);

            await LoadRelatedData(product);

            if (!HasAccessToSee(product, request.RequestOwner)) return UnauthorizedRequest();

            var vm = product.MapToViewModel();
           
            return new ProductResponse() { Access = ResponseAccess.Granted, Product = vm };
        }

        private async Task LoadRelatedData(Product product)
        {
            product.Scheme = await _schemeDataService.GetAsync(product.SchemeId);
            product.PrimaryCategory = await _productCategoryDataService.GetAsync(product.PrimaryCategoryId);
            await product.LoadDataByOptionsAsync(_unitOfWork);
        }

        public async Task<ProductAddResponse> Add(ProductAddRequest request)
        {
            var product = request.ProductToAdd.MapToModel();
            product.Fields = request.ProductToAdd.Fields.MapToModel();
            await base.BaseBeforeAddAsync(product, request.RequestOwner);
            await _productFacade.AddAsync(product);
            await base.BaseAfterAddAsync(product, request.RequestOwner);
            return new ProductAddResponse() { Access = ResponseAccess.Granted };
        }

        public async Task<ProductEditResponse> Edit(ProductEditRequest request)
        {
            var oldProduct = await _productDataService.Query.IncludeNecessaryData().FirstOrDefaultAsync(x => x.Id == request.Id);
            if (!HasAccessToManage(oldProduct, request.RequestOwner)) return new ProductEditResponse() { Access = ResponseAccess.Deny };
            await _productFacade.UpdateAsync(oldProduct, request.Model);
            await base.BaseAfterUpdateAsync(oldProduct, request.RequestOwner);

            return new ProductEditResponse() { Access = ResponseAccess.Granted, Updated = oldProduct.MapToViewModel() };
        }

        public async Task<ProductDeleteResponse> Delete(ProductDeleteRequest request)
        {
            var product = await _productDataService.Query.FirstAsync(x => x.Id == request.Id);
            if (!HasAccessToManage(product, request.RequestOwner)) return new ProductDeleteResponse() { Access = ResponseAccess.Deny };
            await BaseBeforeDeleteAsync(product, request.RequestOwner);
            await _productFacade.DeleteAsync(product);
            await BaseAfterDeleteAsync(product, request.RequestOwner);
            return new ProductDeleteResponse() { Access = ResponseAccess.Granted };
        }
    }
}