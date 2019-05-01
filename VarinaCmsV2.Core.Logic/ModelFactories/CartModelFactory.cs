using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using VarinaCmsV2.Core.Contracts;
using VarinaCmsV2.Core.Contracts.ModelFactories;
using VarinaCmsV2.Core.Logic.Helpers;
using VarinaCmsV2.Core.Services.CurrentUser;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.DomainClasses.Entities.EShop;
using VarinaCmsV2.Services.DataServices;
using VarinaCmsV2.Services.WebClientServices;
using VarinaCmsV2.ViewModel.Eshop;
using VarinaCmsV2.ViewModel.User;

namespace VarinaCmsV2.Core.Logic.ModelFactories
{
    public class CartModelFactory : ICartModelFactory
    {
        private readonly ProductDataService _productDataService;
        private readonly ShoppingCartHelper _shoppingCartHelper;
        private readonly IWorkContext _workContext;
        private readonly ICurrentUserService _currentUserService;

        public CartModelFactory(IWorkContext workContext, ShoppingCartHelper shoppingCartHelper,
            ProductDataService productDataService, ICurrentUserService currentUserService)
        {
            _workContext = workContext;
            _shoppingCartHelper = shoppingCartHelper;
            _productDataService = productDataService;
            _currentUserService = currentUserService;
        }

        public LiquidAdapter GetCurrentUserCartModel()
        {
            _shoppingCartHelper.HandleUserCartWarnings(_workContext.CurrentUser);

            foreach (var item in _workContext.CurrentUser.ShoppingCartItems)
                item.Product.Pictures = item.Product.Pictures.OrderBy(x => x.DisplayOrder).ToList();
            var shoppingCartItemIds = _workContext.CurrentUser.ShoppingCartItems.Select(x => x.Product.Id)
                .ToList();
            return new UserCartLiquidViewModel
            {
                User = _workContext.CurrentUser.AsLiquidAdapted(),
                ShoppingCartItems = _workContext.CurrentUser.ShoppingCartItems.MapToLiquidVeiwModel(),
                CrossSellings = GetProductCrossSellings(shoppingCartItemIds),
                Eshantions = GetProductEshantions(shoppingCartItemIds),
                TotalPrice = _workContext.CurrentUser.ShoppingCartItems.Select(x => x.Product).Sum(x => x.Price),
                CouponCodeUsages = _currentUserService.GetUsedCouponCodeUsages()
            };
        }

        private List<ProductLiquidAdapter> GetProductEshantions(List<Guid> prouctIds)
        {
            return _productDataService.Query.Where(x => prouctIds.Contains(x.Id)).SelectMany(x => x.Eshantions)
                .Where(x => x.Inventory.StockQuantity > 0 || x.Inventory.TrackMethod == InventoryTrackMethod.DoNotTrack)
                .Include(x => x.Pictures).ToList().AsLiquidAdapted();
        }


        private List<ProductLiquidAdapter> GetProductCrossSellings(List<Guid> prouctIds)
        {
            return _productDataService.Query.Where(x => prouctIds.Contains(x.Id)).SelectMany(x => x.CrossSellings)
                .Where(x => x.Inventory.StockQuantity > 0 || x.Inventory.TrackMethod == InventoryTrackMethod.DoNotTrack)
                .Include(x => x.Pictures).ToList().AsLiquidAdapted();
        }
    }
}