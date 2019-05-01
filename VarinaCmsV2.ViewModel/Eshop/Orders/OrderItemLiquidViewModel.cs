using System;
using VarinaCmsV2.DomainClasses.Entities.EShop.Orders;

namespace VarinaCmsV2.ViewModel.Eshop.Orders
{
    public class OrderItemLiquidViewModel
    {
        public Guid ProductId { get; set; }
        public Guid OrderId { get; set; } 
        public int Quantity { get; set; } 
        public long Price { get; set; }
        public long UnitPrice { get; set; }
        public long DiscountAmount { get; set; }
        public int DownloadCount { get; set; }
        public bool IsDownloadActivated { get; set; }
        public ProductLiquidAdapter Product { get; set; }
    }
}