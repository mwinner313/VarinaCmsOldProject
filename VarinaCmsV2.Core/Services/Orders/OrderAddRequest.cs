using System.Security.Principal;
using VarinaCmsV2.ViewModel.Eshop.Orders;

namespace VarinaCmsV2.Core.Services.Orders
{
    public class OrderAddRequest
    {
        public IPrincipal RequestOwner { get; set; }
        public OrderAddOrUpdateViewModel OrderToAdd { get; set; }
    }
}