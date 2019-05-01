using System.Security.Principal;

namespace VarinaCmsV2.Core.Services.Orders
{
    public class OrderListRequest
    {
        public OrderQuery Query { get; set; }
        public IPrincipal RequestOwner { get; set; }
    }
}