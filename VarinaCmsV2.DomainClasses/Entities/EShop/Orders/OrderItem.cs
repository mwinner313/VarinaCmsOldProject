using System;

namespace VarinaCmsV2.DomainClasses.Entities.EShop.Orders
{
    public class OrderItem:BaseEntity
    {
        public Guid ProductId { get; set; }
        public Guid OrderId { get; set; }
        public int Quantity { get; set; }
        public long Price { get; set; }
        public long UnitPrice { get; set; }
        public long DiscountAmount { get; set; }
        public int DownloadCount { get; set; }
        public bool IsDownloadActivated { get; set; }
        public DateTime? DownloadExpirationDate { get; set; }
        #region Navigation
        public Order Order { get; set; }
        public Product Product { get; set; }
        #endregion
    }
}