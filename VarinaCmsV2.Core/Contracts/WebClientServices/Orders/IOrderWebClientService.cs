using System;
using System.Threading.Tasks;
using VarinaCmsV2.DomainClasses.Entities.EShop.Orders;

namespace VarinaCmsV2.Core.Contracts.WebClientServices.Orders
{
    public interface IOrderWebClientService
    {
        Task<Order> PlaceCurrentUserCartAsync();
        Task<Order> PostProcessOrderPaymentAsync(Guid orderId);
        Task CountUpDownloadCountForProductOrderItem(Guid orderId, Guid productId);
    }
}
