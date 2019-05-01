using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.Core.Contracts;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.Core.Contracts.Security;
using VarinaCmsV2.Core.Contracts.WebClientServices.Cart;
using VarinaCmsV2.Core.Contracts.WebClientServices.Products;
using VarinaCmsV2.Data.Contracts;
using VarinaCmsV2.DomainClasses.Entities.EShop;

namespace VarinaCmsV2.Core.Logic.Services.Cart
{
    public class CartService:ICartService
    {
        private readonly IProductDataService _productDataService;
        private readonly IWorkContext _workContext;
        private readonly IRestrictedItemAccessManager _accessManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IShoppingCartHelper _shoppingCartHelper;

        public CartService(IProductDataService productDataService, IWorkContext workContext, IRestrictedItemAccessManager accessManager, IUnitOfWork unitOfWork, IShoppingCartHelper shoppingCartHelper)
        {
            _productDataService = productDataService;
            _workContext = workContext;
            _accessManager = accessManager;
            _unitOfWork = unitOfWork;
            _shoppingCartHelper = shoppingCartHelper;
        }

        public async Task<AddToCartResponse> AddToCartAsync(Guid productId, int quantity)
        {
            var res = new AddToCartResponse();
            var product = _productDataService.Query.Include(x => x.RequiredProducts)
                .FirstOrDefault(x => x.Id == productId);

            if (product == null) return new AddToCartResponse { Message = "محصول یافت نشد" };

            res.Message =
                _shoppingCartHelper.GetAddToCartValidationErrors(_workContext.CurrentUser, product, quantity,
                    _accessManager, _unitOfWork);

            if (string.IsNullOrEmpty(res.Message))
            {
                await _shoppingCartHelper.AddToCartAsync(product, quantity, _workContext.CurrentUser, _unitOfWork);

                res.Success = true;
                res.Message = $"{product.Title} به سبد خرید اضافه شد ";
            }
            return res;
        }

        public async Task<RomveFromCartResponse> RemoveFromCartAsync(Guid productId)
        {
            var res = new RomveFromCartResponse();

            _workContext.CurrentUser.ShoppingCartItems = _unitOfWork.Set<ShoppingCartItem>()
                .Include(x => x.Product)
                .Include(x => x.Product.RequiredProducts)
                .Where(x => x.CreatorId == _workContext.CurrentUser.Id).ToList();

            var product = _productDataService.Query
                .Include(x => x.RequiredProducts)
                .FirstOrDefault(x => x.Id == productId);

            if (product == null) return new RomveFromCartResponse { Message = "محصول یافت نشد" };

            res.Message =
                _shoppingCartHelper.GetRemoveFromCartValidationErrors(_workContext.CurrentUser, product);


            if (string.IsNullOrEmpty(res.Message))
            {
                await _shoppingCartHelper.RemoveFromCart(product, _workContext.CurrentUser, _unitOfWork);

                res.Success = true;
                res.Message = $"از سبد خرید حذف شد {product.Title}";
            }

            return res;
        }

        public async Task UpdateShoppingCartItems(List<ShoppingCartItemUpdateModel> updatedCartItems)
        {
            foreach (var cartItem in _workContext.CurrentUser.ShoppingCartItems)
            {
                var updated = updatedCartItems.FirstOrDefault(x => x.Id == cartItem.Id);
                if (updated != null)
                {
                    cartItem.Quantity = updated.Quantity;
                    _unitOfWork.Update(cartItem);
                }
            }
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
