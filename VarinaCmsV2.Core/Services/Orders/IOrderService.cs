using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.DomainClasses.Entities.EShop.Shipping;

namespace VarinaCmsV2.Core.Services.Orders
{
    public interface IOrderService
    {
        Task<OrderListResponse> Get(OrderListRequest request);
        Task<OrderResponse> Get(OrderGetRequest request);
        Task<OrderEditResponse> Edit(OrderEditRequest request);
        Task ChangeOrderItemDownLoadActivationState(Guid orderItemId);
        Task<SimpleResonse> SendOrderShippmentStatusChangedNotification(Guid orderId);
        Task ChangeSeenStateByAdmin(Guid orderId);
    }
}
