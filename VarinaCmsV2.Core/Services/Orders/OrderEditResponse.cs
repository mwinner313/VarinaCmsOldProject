using System.Collections.Generic;
using System.Net.Http.Headers;
using VarinaCmsV2.ViewModel.Eshop.Orders;

namespace VarinaCmsV2.Core.Services.Orders
{
    public class OrderEditResponse
    {
        public ResponseAccess Access { get; set; }
        public string Message { get; set; } = "";
        public List<OrderLogViewModel> Logs { get; set; }
    }
}