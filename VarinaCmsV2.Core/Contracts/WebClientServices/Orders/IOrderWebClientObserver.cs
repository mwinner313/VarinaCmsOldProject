using System.Threading.Tasks;
using VarinaCmsV2.DomainClasses.Entities.EShop.Orders;

namespace VarinaCmsV2.Core.Contracts.WebClientServices.Orders
{
    public interface IOrderWebClientObserver
    {
        Task OrderPlaced(Order order);
        Task OrderPaid(Order order);
    }
}
