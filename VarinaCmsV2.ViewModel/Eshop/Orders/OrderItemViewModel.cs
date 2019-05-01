using System;

namespace VarinaCmsV2.ViewModel.Eshop.Orders
{
    public class OrderItemViewModel:BaseVeiwModel
    {
        public Guid ProductId { get; set; }
        public Guid OrderId { get; set; }
        public int Quantity { get; set; }
        public long Price { get; set; }
        public long UnitPrice { get; set; }
        public long DiscountAmount { get; set; }
        public int DownloadCount { get; set; }
        public bool IsDownloadActivated { get; set; }
        public OrderedProductViewModel Product { get; set; }
    }
}