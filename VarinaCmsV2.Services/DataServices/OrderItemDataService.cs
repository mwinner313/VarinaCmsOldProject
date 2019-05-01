using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.Data.Contracts;
using VarinaCmsV2.DomainClasses.Entities.EShop.Orders;

namespace VarinaCmsV2.Services.DataServices
{
    public class OrderItemDataService:BaseDataService<OrderItem>, IOrderItemDataService
    {
        public OrderItemDataService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
