using System.Collections.Generic;
using System.Threading.Tasks;
using VarinaCmsV2.DomainClasses.Entities.EShop.Discounts;
using VarinaCmsV2.DomainClasses.Entities.EShop.Orders;

namespace VarinaCmsV2.Core.Contracts.WebClientServices.Discounts
{
    public interface IDiscountWebClientService
    {
        void ApplyNeccesaryDiscountsOnOrder(Order newOrder);
        List<Discount> GetProductAvilableDiscounts(DomainClasses.Entities.EShop.Product product, bool includeRequiresCouponCodes = false);
        void ExpireUsedMultiCouponCodeDiscountAfterPaymentCompeleted(Order order);
    }
}
