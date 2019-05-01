using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.DomainClasses.Entities.EShop.Orders;

namespace VarinaCmsV2.DomainClasses.Entities.EShop.Discounts
{
    public class DiscountUsageHistory:BaseEntity
    {
        public Guid DiscountId { get; set; }
        public Guid OrderId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public  Discount Discount { get; set; }
        public Guid? CouponCodeId { get; set; }
        public  Order Order { get; set; }
    }
}
