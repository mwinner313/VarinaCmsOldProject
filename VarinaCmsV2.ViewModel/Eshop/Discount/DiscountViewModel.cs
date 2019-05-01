using System.Collections.Generic;
using VarinaCmsV2.DomainClasses.Entities.EShop.Discounts;

namespace VarinaCmsV2.ViewModel.Eshop.Discount
{
    public class DiscountViewModel:BaseVeiwModel
    {
        public DiscountLimitationType LimitationType { get; set; }
        public DiscountType DiscountType { get; set; }
        public List<CouponCode> CouponCodes { get; set; }
        public List<ProductCategoryViewModel> AppliedToCategories { get; set; }
        public List<ProductViewModel> AppliedToProducts { get; set; }
        public string Name { get; set; }
        public long? MinimumCartAmountRequirement { get; set; }
        public bool UsePercentage { get; set; }
        public long DiscountPercentage { get; set; }
        public long DiscountAmount { get; set; }
        public long? MaximumDiscountAmount { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public bool RequiresCouponCode { get; set; }
        public string CouponCode { get; set; }
        public bool IsCumulative { get; set; }
        public int LimitationTimes { get; set; }
        public int? MaximumDiscountedQuantity { get; set; }
        public bool AppliedToSubCategories { get; set; }
        public CouponCodeType CouponCodeType { get; set; }
    }
}