using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using VarinaCmsV2.Common.Web;
using VarinaCmsV2.Core.Contracts;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.Core.Contracts.ModelFactories;
using VarinaCmsV2.Core.Contracts.WebClientServices.Orders;
using VarinaCmsV2.Core.Contracts.WebClientServices.Products;
using VarinaCmsV2.Core.CustomAttributes;
using VarinaCmsV2.Core.Services.CurrentUser;
using VarinaCmsV2.Core.Web;
using VarinaCmsV2.Core.Web.Security.Mvc;
using VarinaCmsV2.ViewModel.User;

namespace VarinaCmsV2.Web.Controllers
{
    [DenySearchEngineRequest]
    [MvcCmsAuthorize]
    public class CheckOutController : LiquidController
    {
        private readonly IWorkContext _workContext;
        private readonly ICurrentUserService _currentUserService;
        private readonly IOrderWebClientService _orderWebClientService;
        private readonly IShoppingCartHelper _shoppingCartHelper;
        private readonly ICheckoutModelFactory _checkoutModelFactory;
        public CheckOutController(IWorkContext workContext, IShoppingCartHelper shoppingCartHelper, ICheckoutModelFactory checkoutModelFactory, ICurrentUserService currentUserService)
        {
            _workContext = workContext;
            _shoppingCartHelper = shoppingCartHelper;
            _checkoutModelFactory = checkoutModelFactory;
            _currentUserService = currentUserService;
        }
        [MvcCmsAuthorize]
        public async Task<ActionResult> Index()
        {
            if (!_workContext.CurrentUser.ShoppingCartItems.Any())
                return RedirectToAction("Index", "Cart");

            _shoppingCartHelper.HandleUserCartWarnings(_workContext.CurrentUser);

            if (_workContext.CurrentUser.ShoppingCartItems.SelectMany(x => x.Warnings).Any())
                return RedirectToAction("Index", "Cart");

            if(_currentUserService.GetUsedCouponCodeUsages().Any(x=>x.Expired))
                return RedirectToAction("Index", "Cart");

            if (_workContext.CurrentUser.ShoppingCartItems.Any(x => x.Product.Shipment.IsShipEnabled))
                return RedirectToAction("Shipping");

            return RedirectToAction("Payment");
        }
        [MvcCmsAuthorize]

        public ActionResult Shipping()
        {
            if (!_workContext.CurrentUser.ShoppingCartItems.Any())
                return RedirectToAction("Index", "Cart");

            _shoppingCartHelper.HandleUserCartWarnings(_workContext.CurrentUser);

            if (_workContext.CurrentUser.ShoppingCartItems.SelectMany(x => x.Warnings).Any())
                return RedirectToAction("Index", "Cart");

            if (_currentUserService.GetUsedCouponCodeUsages().Any(x => x.Expired))
                return RedirectToAction("Index", "Cart");

            if (!_workContext.CurrentUser.ShoppingCartItems.Any(x => x.Product.Shipment.IsShipEnabled))
                return RedirectToAction("Payment");

            return LiquidView(_checkoutModelFactory.PrepareShippingPageViewData(), "shipping");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [MvcCmsAuthorize]
        public async Task<ActionResult> Shipping(Guid selectedAddress, Guid? selectedShippingMethod)
        {
            if (!_workContext.CurrentUser.ShoppingCartItems.Any())
                return RedirectToAction("Index", "Cart");

            _shoppingCartHelper.HandleUserCartWarnings(_workContext.CurrentUser);

            if (_workContext.CurrentUser.ShoppingCartItems.SelectMany(x => x.Warnings).Any())
                return RedirectToAction("Index", "Cart");

            if (_currentUserService.GetUsedCouponCodeUsages().Any(x => x.Expired))
                return RedirectToAction("Index", "Cart");

            await _currentUserService.SaveCustomAttributeAsync(UserAttributes.SelectedShippingAddressId,
                   selectedAddress.ToString());
            if (selectedShippingMethod.HasValue)
                await _currentUserService.SaveCustomAttributeAsync(UserAttributes.SelectedShippingMethodId,
                      selectedShippingMethod.ToString());

            return RedirectToAction("Payment");
        }


        [MvcCmsAuthorize]
        public ActionResult Payment()
        {
            if (!_workContext.CurrentUser.ShoppingCartItems.Any())
                return RedirectToAction("Index", "Cart");

            _shoppingCartHelper.HandleUserCartWarnings(_workContext.CurrentUser);

            if (_workContext.CurrentUser.ShoppingCartItems.SelectMany(x => x.Warnings).Any())
                return RedirectToAction("Index", "Cart");

            if (_currentUserService.GetUsedCouponCodeUsages().Any(x => x.Expired))
                return RedirectToAction("Index", "Cart");

            _orderWebClientService.PlaceCurrentUserCartAsync();

            return LiquidView(_checkoutModelFactory.PreparePaymentPageViewData(), "payment");
        }
      
        [MvcCmsAuthorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> NewShippingAddress(AddressAddOrUpdateModel model)
        {
            if (ModelState.IsValid)
                await _currentUserService.AddNewAddressAsync(model);
            if (Request.IsAjaxRequest()) return Json(new { success = ModelState.IsValid });

            return RedirectToAction("Shipping");
        }
        [MvcCmsAuthorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateShippingAddress(AddressAddOrUpdateModel model)
        {
            if (ModelState.IsValid)
                await _currentUserService.UpdateAddress(model);

            if (Request.IsAjaxRequest()) return Json(new { success = ModelState.IsValid });

            return RedirectToAction("Shipping");
        }

        [MvcCmsAuthorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteShippingAddress(Guid id)
        {
            await _currentUserService.DeleteAddress(id);

            if (Request.IsAjaxRequest()) return Json(new { success = true });

            return RedirectToAction("Shipping");
        }
    }
}