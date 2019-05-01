using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using VarinaCmsV2.Common.MVC;
using VarinaCmsV2.Common.Web;
using VarinaCmsV2.Core.Contracts.ModelFactories;
using VarinaCmsV2.Core.Contracts.WebClientServices.Cart;
using VarinaCmsV2.Core.Contracts.WebClientServices.Products;
using VarinaCmsV2.Core.CustomAttributes;
using VarinaCmsV2.Core.Services.CurrentUser;
using VarinaCmsV2.Core.Web;
using VarinaCmsV2.Core.Web.Security.Mvc;

namespace VarinaCmsV2.Web.Controllers
{
    [DenySearchEngineRequest]
    [AllowAnonymous]
    public class CartController : LiquidController
    {
        private readonly ICartModelFactory _cartModelFactory;
        private readonly ICartService _cartService;
        private readonly ICurrentUserService _currentUserService;

        public CartController(IProductWebClientService productWebClientService,
            ICartModelFactory cartModelFactory, ICurrentUserService currentUserService
            ,ICartService cartService)
        {
            _cartService = cartService;
            _cartModelFactory = cartModelFactory;
            _currentUserService = currentUserService;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            return LiquidView(_cartModelFactory.GetCurrentUserCartModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [MvcCmsAuthorize]
        public async Task<ActionResult> ProceedWithOrder(List<ShoppingCartItemUpdateModel> cartItems)
        {
            await _cartService.UpdateShoppingCartItems(cartItems);
            return RedirectToAction("Index", "CheckOut");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [MvcCmsAuthorize]
        public async Task<ActionResult> ApplyCouponCode(string coupon)
        {
            if (string.IsNullOrEmpty(coupon))
                return Json(new {success = false, Message = "کد کپن وارد نشده"});

            var res = await _currentUserService.AddCouponCodeUsage(coupon);
            return Json(res);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [MvcCmsAuthorize]
        public async Task<ActionResult> DeleteCouponCode(string coupon)
        {
            await _currentUserService.DeleteCustomAttributeAsync(UserAttributes.CouponCodeUsage + coupon);

            if (Request.IsAjaxRequest()) return Json(new {success = true});

            return RedirectToAction("Index");
        }

        [HttpPost]
        [AjaxRequest]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add(Guid productId, int quantity = 0)
        {
            var result = _cartService.AddToCartAsync(productId, quantity);
            return Json(await result);
        }

        [HttpPost]
        [AjaxRequest]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Remove(Guid productId)
        {
            var result = _cartService.RemoveFromCartAsync(productId);

            return Json(await result);
        }
    }
}