using System.Collections.Generic;
using System.Linq;
using VarinaCmsV2.DomainClasses.Entities.EShop.Orders;
using VarinaCmsV2.ViewModel.Eshop.Orders;

namespace VarinaCmsV2.Core.Services.Orders
{
    public class OrderListResponse : IServiceResponse
    {
        public ResponseAccess Access { get; set; }
        public string Message { get; set; }
        public List<OrderViewModel> Items { get; set; }
        public long Count { get; set; }
    }
}