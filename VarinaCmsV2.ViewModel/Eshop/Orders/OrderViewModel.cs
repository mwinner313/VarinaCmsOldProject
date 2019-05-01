using System;
using System.Collections.Generic;
using VarinaCmsV2.DomainClasses.Entities.EShop.Orders;
using VarinaCmsV2.DomainClasses.Entities.EShop.Shipping;
using VarinaCmsV2.ViewModel.User;

namespace VarinaCmsV2.ViewModel.Eshop.Orders
{
    public class OrderViewModel:BaseVeiwModel
    {
        public string UpdateDateTime { get; set; }
        public string CreateDateTime { get; set; }
        public Guid CreatorId { get; set; }
        public string PaidDate { get; set; }
        public Guid? ShippingAddressId { get; set; }
        public Guid? ShipmentId { get; set; }
        public bool IsDeleted { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public ShippingStatus ShippingStatus { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentTrackNumber { get; set; }
        public long OrderTrackNumber { get; set; }
        public string ShippingMethod { get; set; }
        public long OrderDiscount { get; set; }
        public long OrderTotal { get; set; }
        public string Comment { get; set; }

        public List<DiscountUsageHistoryViewModel> DiscountUsageHistories { get; set; }
        public List<OrderItemViewModel> OrderItems { get; set; }
        public List<OrderLogViewModel> OrderLogs { get; set; }
        public ShipmentViewModel Shipment { get; set; }
        public AddressViewModel ShippingAddress { get; set; }
        public UserWebClientViewModel Creator { get; set; }
    }
}
