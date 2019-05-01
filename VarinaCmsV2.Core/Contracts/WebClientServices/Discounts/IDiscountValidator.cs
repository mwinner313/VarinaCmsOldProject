using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.DomainClasses.Entities.EShop.Discounts;

namespace VarinaCmsV2.Core.Contracts.WebClientServices.Discounts
{
    public interface IDiscountValidator
    {
        bool CanUseBaseOnStartAndEndDate(Discount discount);
        bool CanUseBaseOnLimitation(Discount discount, Guid currUserId, CouponUsageResponse res = null);
        bool CanUseInCartBaseOnOrderTotal(Discount discount, CouponUsageResponse res = null);
        bool CanUseInCartBaseOnProductCategoryAndSku(Discount discount, CouponUsageResponse res = null);
    }
}
