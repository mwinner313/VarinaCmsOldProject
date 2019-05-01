using VarinaCmsV2.Common;
using VarinaCmsV2.DomainClasses.Entities.EShop.Discounts;

namespace VarinaCmsV2.Core.Services.Discounts
{
    public class DiscountQuery:Pagenation
    {
        public string Name { get; set; }
        public DiscountType? DiscountType { get; set; }
        public string CouponCode { get; set; }
    }
}