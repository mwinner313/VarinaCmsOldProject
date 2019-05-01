using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.Core.Contracts.WebClientServices.Discounts;
using VarinaCmsV2.Core.Services.CurrentUser;
using VarinaCmsV2.Data.Contracts;
using VarinaCmsV2.DomainClasses.Entities.EShop;
using VarinaCmsV2.DomainClasses.Entities.EShop.Discounts;
using VarinaCmsV2.DomainClasses.Entities.EShop.Orders;
using VarinaCmsV2.Services.Helpers;

namespace VarinaCmsV2.Services.WebClientServices
{
    public class DiscountWebClientService : IDiscountWebClientService
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly DiscountApplierHelper _discountApplierHelper;
        private readonly IDiscountDataService _discountDataService;
        private readonly IDiscountValidator _discountValidator;
        private readonly IProductCategoryDataService _productCategoryDataService;
        private readonly IProductDataService _productDataService;
        private readonly IUnitOfWork _unitOfWork;

        public DiscountWebClientService(IDiscountDataService discountDataService,
            ICurrentUserService currentUserService,
            IProductDataService productDataService, IProductCategoryDataService productCategoryDataService,
            IDiscountValidator discountValidator, IUnitOfWork unitOfWork)
        {
            _discountDataService = discountDataService;
            _currentUserService = currentUserService;
            _productDataService = productDataService;
            _productCategoryDataService = productCategoryDataService;
            _discountValidator = discountValidator;
            _unitOfWork = unitOfWork;
            _discountApplierHelper = new DiscountApplierHelper();
        }
        public void ApplyNeccesaryDiscountsOnOrder(Order newOrder)
        {
            if (!_discountDataService.Query.FilterByAvailibilty().Any()) return;
            ApplyUserCouponCodes(newOrder);
            ApplyWebsiteAvailableDiscounts(newOrder);
        }
        public List<Discount> GetProductAvilableDiscounts(Product product, bool includeRequiresCouponCodes = false)
        {
            var retVal = new List<Discount>();

            var discounts = _discountDataService.Query
                .FilterByAvailibilty().Include(x => x.AppliedToCategories).Include(x => x.AppliedToProducts)
                .Where(x => x.DiscountType == DiscountType.AssignedToSkus ||
                            x.DiscountType == DiscountType.AssignedToCategories)
                .Where(x => !x.RequiresCouponCode || includeRequiresCouponCodes)
                .ToList();

            foreach (var discount in discounts.Where(x => x.DiscountType == DiscountType.AssignedToCategories).ToList())
                if (discount.GetAllDiscountAppliedCategories(_productCategoryDataService)
                    .Any(x => x.Id == product.PrimaryCategoryId))
                    retVal.Add(discount);

            foreach (var discount in discounts.Where(x => x.DiscountType == DiscountType.AssignedToSkus).ToList())
                if (discount.AppliedToProducts.Any(x => x.Id == product.Id))
                    retVal.Add(discount);

            return retVal;
        }
        public void ExpireUsedMultiCouponCodeDiscountAfterPaymentCompeleted(Order order)
        {
            foreach (var usageHistory in order.DiscountUsageHistories)
            {
                if (usageHistory.Discount.RequiresCouponCode &&
                    usageHistory.Discount.CouponCodeType == CouponCodeType.Multi)
                {
                    var couponCode = usageHistory.Discount.CouponCodes.First(x => x.Id == usageHistory.CouponCodeId);
                    couponCode.Reserved = false;
                    couponCode.UsedById = order.CreatorId;
                    _unitOfWork.Update(couponCode);
                }
            }
        }
        private void ApplyWebsiteAvailableDiscounts(Order newOrder)
        {
            ApplyGlobalProductDiscounts(newOrder);
            ApplyAssinedToShippingAndOrderSubtotal(newOrder);
        }
        private void ApplyAssinedToShippingAndOrderSubtotal(Order newOrder)
        {
            _discountDataService.Query
                .FilterByAvailibilty()
                .Where(x => (x.DiscountType == DiscountType.AssignedToOrderSubTotal ||
                             x.DiscountType == DiscountType.AssignedToShipping) && !x.RequiresCouponCode)
                .ToList().ForEach(x =>
                {
                    if (_discountValidator.CanUseInCartBaseOnOrderTotal(x) &&
                        _discountValidator.CanUseBaseOnLimitation(x, newOrder.CreatorId))
                        _discountApplierHelper.ApplyOnOrder(newOrder, x);
                    AddUsageHistory(newOrder, x);
                    HandleDiscountReserve(x);
                });
        }
        private void ApplyGlobalProductDiscounts(Order newOrder)
        {
            foreach (var item in newOrder.OrderItems)
            {
                var productDiscounts = GetProductAvilableDiscounts(item.Product);
                foreach (var discount in productDiscounts)
                    if (_discountValidator.CanUseInCartBaseOnProductCategoryAndSku(discount) &&
                        _discountValidator.CanUseBaseOnLimitation(discount, newOrder.CreatorId))
                    {
                        _discountApplierHelper.ApplyOnOrderItem(item, discount);
                        AddUsageHistory(newOrder, discount);
                        HandleDiscountReserve(discount);
                    }
            }
        }
        private void ApplyUserCouponCodes(Order newOrder)
        {
            _currentUserService.GetUsedCouponCodeDiscounts()
                .ToList()
                .ForEach(discount =>
                {
                    if (discount.DiscountType == DiscountType.AssignedToCategories)
                        ApplyByCategory(newOrder, discount);

                    if (discount.DiscountType == DiscountType.AssignedToSkus)
                        ApplyBySkus(newOrder, discount);

                    if (discount.DiscountType == DiscountType.AssignedToOrderSubTotal ||
                        discount.DiscountType == DiscountType.AssignedToShipping)
                        _discountApplierHelper.ApplyOnOrder(newOrder, discount);

                    AddUsageHistoryIncludeCoupponCode(newOrder, discount);
                    HandleDiscountReserve(discount);
                });
        }
        private void ApplyBySkus(Order newOrder, Discount discount)
        {
            var appliedProductIds =
                _productDataService.Query.Where(x => x.AppliedDiscounts.Any(d => d.Id == discount.Id))
                    .Select(x => x.Id).ToList();
            foreach (var orderItem in newOrder.OrderItems)
                if (appliedProductIds.Any(x => x == orderItem.ProductId))
                    _discountApplierHelper.ApplyOnOrderItem(orderItem, discount);
        }
        private void ApplyByCategory(Order newOrder, Discount discount)
        {
            var appliedCategories = discount.GetAllDiscountAppliedCategories(_productCategoryDataService);

            foreach (var orderItem in newOrder.OrderItems)
                if (appliedCategories.Any(x => x.Id == orderItem.Product.PrimaryCategoryId))
                    _discountApplierHelper.ApplyOnOrderItem(orderItem, discount);
        }
        private void AddUsageHistoryIncludeCoupponCode(Order newOrder, Discount discount)
        {
            newOrder.DiscountUsageHistories.Add(new DiscountUsageHistory
            {
                DiscountId = discount.Id,
                CreateDateTime = DateTime.Now,
                CouponCodeId = GetUsedCouponCodeForMultiCouponCodeDiscount(discount)
            });
        }
        private void AddUsageHistory(Order newOrder, Discount discount)
        {
            newOrder.DiscountUsageHistories.Add(new DiscountUsageHistory
            {
                DiscountId = discount.Id,
                CreateDateTime = DateTime.Now
            });
        }
        private Guid? GetUsedCouponCodeForMultiCouponCodeDiscount(Discount discount)
        {
            if (discount.CouponCodeType != CouponCodeType.Multi) return null;
            return discount.CouponCodes.First(x =>
                _currentUserService.GetUsedCouponCodeUsages().Select(c => c.Code).Contains(x.Code)).Id;
        }

        private void HandleDiscountReserve(Discount discount)
        {
            if (discount.RequiresCouponCode && discount.CouponCodeType == CouponCodeType.Multi)
            {
                var couponCodeUsages = _currentUserService.GetUsedCouponCodeUsages().Select(x => x.Code);
                var couoponCode = discount.CouponCodes.First(x => couponCodeUsages.Contains(x.Code));
                couoponCode.Reserved = true;
                _unitOfWork.Update(couoponCode);
            }
        }
    }
}