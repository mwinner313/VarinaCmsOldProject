using VarinaCmsV2.Common;
using VarinaCmsV2.DomainClasses.Entities.EShop.Orders;
using VarinaCmsV2.DomainClasses.Entities.EShop.Shipping;

namespace VarinaCmsV2.Core.Services.Orders
{
    public class OrderQuery : Pagenation
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string ProductTitle { get; set; }
        public string UserName { get; set; }
        public PaymentStatusOption PaymentStatus { get; set; }
        public ShippingStatusOption ShippingStatus { get; set; }
        public OrderStatusOption OrderStatus { get; set; }
        public bool NotActivatedDownloads { get; set; }
        public bool NotSeen { get; set; }

       
    }
    public enum OrderStatusOption
    {
        All = 5,
        Pending = 10,
        Processing = 20,
        Complete = 30,
        Cancelled = 40
    }

    public enum ShippingStatusOption
    {
        All = 5,
        ShippingNotRequired = 10,
        NotYetShipped = 20,
        Shipped = 30,
        Delivered = 40,
    }

    public enum PaymentStatusOption
    {
        All = 5,
        Pending = 10,
        Paid = 20,
    }
}