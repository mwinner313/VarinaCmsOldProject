using System;
using System.Linq;
using VarinaCmsV2.Core.Contracts;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.Core.Contracts.WebClientServices.Discounts;
using VarinaCmsV2.DomainClasses.Entities.EShop.Discounts;
using VarinaCmsV2.DomainClasses.Entities.EShop.Orders;
using VarinaCmsV2.Security.Contracts;
using VarinaCmsV2.Services.Helpers;

namespace VarinaCmsV2.Services.WebClientServices
{
    public class DiscountValidator : IDiscountValidator
    {
        private readonly IDiscountDataService _discountDataService;
        private readonly IDiscountUsageDataService _discountUsageDataService;
        private readonly IIdentityManager _identityManager;
        private readonly IProductCategoryDataService _productCategoryDataService;
        private readonly IProductDataService _productDataService;
        private readonly IWorkContext _workContext;

        public DiscountValidator(IDiscountDataService discountDataService,
            IDiscountUsageDataService discountUsageDataService, IWorkContext workContext,
            IIdentityManager identityManager, IProductCategoryDataService productCategoryDataService,
            IProductDataService productDataService)
        {
            _discountDataService = discountDataService;
            _discountUsageDataService = discountUsageDataService;
            _workContext = workContext;
            _identityManager = identityManager;
            _productCategoryDataService = productCategoryDataService;
            _productDataService = productDataService;
        }

        public bool CanUseBaseOnStartAndEndDate(Discount discount)
        {
            if (discount.StartDate.HasValue && DateTime.Now < discount.StartDate.Value)
                return false;

            if (discount.EndDate.HasValue && DateTime.Now > discount.EndDate.Value)
                return false;
            return true;
        }

        public bool CanUseBaseOnLimitation(Discount discount, Guid currUserId, CouponUsageResponse res = null)
        {
            switch (discount.LimitationType)
            {
                case DiscountLimitationType.NTimesOnly:
                {
                    var usedTimes = _discountUsageDataService.Query.Count(x =>
                        x.DiscountId == discount.Id && x.Order.OrderStatus != OrderStatus.Cancelled);
                    if (usedTimes >= discount.LimitationTimes)
                    {
                        if (res != null) res.Message = "تعداد استفاده از این کد تخفیف به پایان رسید";
                        return false;
                    }
                }
                    break;
                case DiscountLimitationType.NTimesPerCustomer:
                {
                    if (_identityManager.GetCurrentIdentity().IsAuthenticated)
                    {
                        var usedTimes = _discountUsageDataService.Query.Count(x =>
                            x.DiscountId == discount.Id && x.Order.CreatorId == currUserId &&
                            x.Order.OrderStatus != OrderStatus.Cancelled);

                        if (usedTimes >= discount.LimitationTimes)
                        {
                            if (res != null) res.Message = "تعداد استفاده از این کد تخفیف برای شما به پایان رسید";
                            return false;
                        }
                    }
                }
                    break;
            }
            return true;
        }

        public bool CanUseInCartBaseOnOrderTotal(Discount discount, CouponUsageResponse res = null)
        {
            if (discount.DiscountType == DiscountType.AssignedToOrderSubTotal &&
                discount.MinimumCartAmountRequirement.HasValue)
            {
                var cartTotal = _workContext.CurrentUser.ShoppingCartItems.Sum(x => x.Product.Price * x.Quantity);
                if (cartTotal < discount.MinimumCartAmountRequirement)
                {
                    if (res != null) res.Message = "این تخفیف شامل حال سبد خرید شما نمیشود";
                    return false;
                }
            }
            return true;
        }

        public bool CanUseInCartBaseOnProductCategoryAndSku(Discount discount, CouponUsageResponse res = null)
        {
            if (discount.DiscountType == DiscountType.AssignedToSkus)
            {
                var shoppingCartItemIds = _workContext.CurrentUser.ShoppingCartItems.Select(x => x.ProductId);
                if (!_discountDataService.Query.Any(x =>
                    x.Id == discount.Id && x.AppliedToProducts.Any(p => shoppingCartItemIds.Contains(p.Id))))
                {
                    if (res != null) res.Message = "این تخفیف شامل حال سبد خرید شما نمیشود";
                    return false;
                }
            }

            if (discount.DiscountType == DiscountType.AssignedToCategories)
            {
                var allCatIds = discount.GetAllDiscountAppliedCategories(_productCategoryDataService).Select(x => x.Id);
                if (!_productDataService.Query.Any(x => allCatIds.Contains(x.PrimaryCategoryId)))
                {
                    if (res != null) res.Message = "این تخفیف شامل حال سبد خرید شما نمیشود";
                    return false;
                }
            }
            return true;
        }
    }
}