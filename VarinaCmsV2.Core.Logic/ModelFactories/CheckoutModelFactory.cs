using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ipg.Token;
using VarinaCmsV2.Common;
using VarinaCmsV2.Core.Contracts;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.Core.Contracts.ModelFactories;
using VarinaCmsV2.Core.CustomAttributes;
using VarinaCmsV2.Core.Logic.Helpers;
using VarinaCmsV2.Core.Services.CurrentUser;
using VarinaCmsV2.Core.Services.Settings;
using VarinaCmsV2.Core.Settings;
using VarinaCmsV2.Data.Contracts;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.DomainClasses.Entities.EShop.Shipping;
using VarinaCmsV2.DomainClasses.Users;
using VarinaCmsV2.ViewModel.Eshop.Shipment;

namespace VarinaCmsV2.Core.Logic.ModelFactories
{
    public class CheckoutModelFactory : ICheckoutModelFactory
    {
        private readonly IWorkContext _workContext;
        private readonly UrlHelper _urlHelper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISettingService _settingService;
        private readonly ICurrentUserService _currentUserService;
        private readonly IOrderDataService _orderDataService;
        public CheckoutModelFactory(IWorkContext workContext, IUnitOfWork unitOfWork, IOrderDataService orderDataService, ICurrentUserService currentUserService, ISettingService settingService)
        {
            _workContext = workContext;
            _unitOfWork = unitOfWork;
            _orderDataService = orderDataService;
            _currentUserService = currentUserService;
            _settingService = settingService;
            _urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);
        }

        public LiquidAdapter PrepareShippingPageViewData()
        {
            var selectedShippingMethodId =
                _workContext.CurrentUser.Attributes.FirstOrDefault(x => x.Name == UserAttributes.SelectedShippingMethodId)
                    ?.Value.ToGuid();

            var selectedShippingAddressId =
                _workContext.CurrentUser.Attributes.FirstOrDefault(x => x.Name == UserAttributes.SelectedShippingAddressId)
                    ?.Value.ToGuid();

            var availibleShippingMethods = _unitOfWork.Set<ShippingMethod>().ToList();
            var availibleStateProvinces = _unitOfWork.Set<StateProvince>().ToList();

            var model = new ShippingPageLiquidViewDataModel()
            {
                StateProvinces = availibleStateProvinces.MapToLiquidVeiwModel(),
                ShippingItems = _workContext.CurrentUser.ShoppingCartItems.MapToLiquidVeiwModel(),
                AvailibleAddresses = _workContext.CurrentUser.Addresses.ToList().MapToLiquidViewModel(),
                SelectedAddressId = selectedShippingAddressId,
                ShippingMethods = availibleShippingMethods.MapToLiquidViewModel(),
                SelectedShippinMethodId = availibleShippingMethods.Any(x => x.Id == selectedShippingMethodId)
                        ? selectedShippingMethodId.Value
                        : Guid.Empty
            };
            return model;
        }

        public LiquidAdapter PreparePaymentPageViewData()
        {
            var shaparakSetting = _settingService.GetSetting<ShaprakGateWaySetting>();

            if (shaparakSetting is null) throw new ArgumentException(nameof(ShaprakGateWaySetting));

            var orderId = GetCurrentUserPaymentProccessingOrderId();
            var order = _orderDataService.Query.First(x => x.Id == orderId);
            var paymentAmount = Convert.ToInt32(order.OrderTotal).ToString();

            TokensClient client = new TokensClient();

            tokenResponse tokenResp = client.MakeToken(paymentAmount, shaparakSetting.MerchantId, order.Id.ToString(), order.PaymentTrackNumber, "", _urlHelper.Action("Index", "Payment", null, HttpContext.Current.Request.Url.Scheme), "");

            return new PaymentPageLiquidViewData()
            {
                ClientToken = tokenResp.token,
                MerchantId = shaparakSetting.MerchantId,
                PaymentId = order.PaymentTrackNumber
            };
        }

        private Guid? GetCurrentUserPaymentProccessingOrderId()
        {
            var orderId = _currentUserService
                .GetCustomAttribute(UserAttributes.PaymentProccessingOrderId)?.Value.ToGuid();

            if (!orderId.HasValue)
                throw new ArgumentException("PaymentProccessingOrderId");
            return orderId;
        }
    }
}
