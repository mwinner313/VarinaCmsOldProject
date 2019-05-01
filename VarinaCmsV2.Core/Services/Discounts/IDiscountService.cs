using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.Core.Contracts.WebClientServices.Discounts;
using VarinaCmsV2.DomainClasses.Entities.EShop.Discounts;
using VarinaCmsV2.DomainClasses.Entities.EShop.Orders;

namespace VarinaCmsV2.Core.Services.Discounts
{
    public interface IDiscountService
    {
        Task<DiscountListResponse> Get(DiscountListRequest request);
        Task<DiscountResponse> Get(DiscountRequest request);
        Task<DiscountAddResponse> Add(DiscountAddRequest request);
        Task<DisountEditResponse> Edit(DisocuntEditRequest request);
        Task<DisountDeleteResponse> Delete(DisocuntDeleteRequest request);
    }
}
