using VarinaCmsV2.ViewModel.Eshop.Orders;

namespace VarinaCmsV2.Core.Services.Orders
{
    public class OrderResponse : IServiceResponse
    {
        public ResponseAccess Access { get; set; }
        public string Message { get; set; }
        public OrderViewModel Order { get; set; }
    }
}