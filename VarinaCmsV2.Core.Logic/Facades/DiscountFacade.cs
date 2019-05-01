using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.Core.Logic.Helpers;
using VarinaCmsV2.Core.LogicFacade;
using VarinaCmsV2.Data.Contracts;
using VarinaCmsV2.DomainClasses.Entities.EShop;
using VarinaCmsV2.DomainClasses.Entities.EShop.Discounts;
using VarinaCmsV2.ViewModel.Eshop.Discount;

namespace VarinaCmsV2.Core.Logic.Facades
{
    public class DiscountFacade : IDiscountFacade
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductDataService _productDataService;
        private readonly IDiscountDataService _discountDataService;
        private readonly IProductCategoryDataService _productCategoryDataService;
        public DiscountFacade(IUnitOfWork unitOfWork, IDiscountDataService discountDataService, IProductDataService productDataService, IProductCategoryDataService productCategoryDataService)
        {
            _unitOfWork = unitOfWork;
            _discountDataService = discountDataService;
            _productDataService = productDataService;
            _productCategoryDataService = productCategoryDataService;
        }

        public Task AddAsync(Discount discount)
        {
            ValidateDiscount(discount);
            HandleAppliedToProducts(discount, discount.AppliedToProducts);
            HandleAppliedToProductCategories(discount, discount.AppliedToCategories);
            return _discountDataService.AddAsync(discount);
        }
        public Task UpadateAsync(Discount discount, DiscountAddOrUpdateModel updated)
        {
            updated.MapToExisting(discount);
            HandleAppliedToProducts(discount, updated.AppliedToProducts.ToList().MapToModel());
            HandleAppliedToProductCategories(discount, updated.AppliedToCategories.ToList().MapToModel());
            ValidateDiscount(discount);
            return _discountDataService.UpdateAsync(discount);
        }
        private void HandleAppliedToProductCategories(Discount discount, ICollection<ProductCategory> appliedToCategories)
        {
            if (discount.DiscountType == DiscountType.AssignedToCategories && appliedToCategories != null && appliedToCategories.Any())
            {
                var catIds = appliedToCategories.Select(x => x.Id);
                discount.AppliedToCategories = _productCategoryDataService.Query.Where(x => catIds.Contains(x.Id)).ToList();
            }
            else
            {
                discount.AppliedToCategories?.Clear();
            }
        }

        private void HandleAppliedToProducts(Discount discount, ICollection<Product> appliedProducts)
        {
            if (discount.DiscountType == DiscountType.AssignedToSkus && appliedProducts != null && appliedProducts.Any())
            {
                var productIds = appliedProducts.Select(x => x.Id);
                discount.AppliedToProducts = _productDataService.Query.Where(x => productIds.Contains(x.Id)).ToList();
            }
            else
            {
                discount.AppliedToProducts?.Clear();
            }

        }
        private static void ValidateDiscount(Discount discount)
        {
            if (!(discount.DiscountType == DiscountType.AssignedToCategories ||
                  discount.DiscountType == DiscountType.AssignedToSkus))
            {
                discount.MaximumDiscountedQuantity = null;
            }
            if (discount.UsePercentage)
            {
                discount.DiscountAmount = 0;
                if (discount.DiscountPercentage < 0 || discount.DiscountPercentage > 100)
                {
                    throw new InvalidOperationException("discount percentage is out of range");
                }
            }
            else
            {
                discount.DiscountPercentage = 0;
            }

            if (discount.DiscountType != DiscountType.AssignedToCategories)
            {
                discount.AppliedToSubCategories = false;
                discount.AppliedToCategories?.Clear();
            }

            if (discount.DiscountType != DiscountType.AssignedToSkus)
            {
                discount.AppliedToProducts?.Clear();
            }


            if (discount.LimitationType != DiscountLimitationType.Unlimited)
            {
                if (discount.LimitationTimes <= 0) throw new InvalidOperationException("limitation times is missing");
            }
            else
            {
                discount.LimitationTimes = 0;
            }

            if (discount.RequiresCouponCode && discount.CouponCodeType == CouponCodeType.Single && string.IsNullOrEmpty(discount.CouponCode))
            {
                throw new InvalidOperationException("mising copon code");
            }

            if (discount.RequiresCouponCode && discount.CouponCodeType == CouponCodeType.Multi)
            {
                discount.CouponCode = null;
            }

        }

    }
}
