using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.DomainClasses.Entities.EShop;
using VarinaCmsV2.DomainClasses.Entities.EShop.Discounts;

namespace VarinaCmsV2.ViewModel.Eshop.Discount
{
    public class DiscountAddOrUpdateModel : BaseVeiwModel
    {
        public string Name { get; set; }
        public bool UsePercentage { get; set; }
        public int DiscountPercentage { get; set; }
        public long DiscountAmount { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public bool RequiresCouponCode { get; set; }
        public string CouponCode { get; set; }
        public int LimitationTimes { get; set; }
        public int? MaximumDiscountedQuantity { get; set; }
        public long? MinimumCartAmountRequirement { get; set; }

        public CouponCodeType CouponCodeType { get; set; } = CouponCodeType.Single;
        public bool AppliedToSubCategories { get; set; }
        public DiscountLimitationType LimitationType { get; set; }
        public DiscountType DiscountType { get; set; }
        public List<ProductCategoryViewModel> AppliedToCategories { get; set; }
        public ICollection<ProductViewModel> AppliedToProducts { get; set; }
        public ICollection<CouponCode> CouponCodes { get; set; }
    }
}
