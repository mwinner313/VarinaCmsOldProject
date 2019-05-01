using System;
using VarinaCmsV2.DomainClasses.Users;

namespace VarinaCmsV2.DomainClasses.Entities.EShop.Discounts
{
    public class CouponCode:BaseEntity
    {
        public string Code { get; set; }
        public Guid? UsedById { get; set; }
        public Discount Discount { get; set; }
        public Guid DiscountId { get; set; }
        public User UsedBy { get; set; }
        public bool Reserved { get; set; }
//        public Portal Portal { get; set; }
//        public Guid PortalId { get; set; }
    }
}