using System;
using System.Collections.Generic;

using DotLiquid;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.DomainClasses.Entities.EShop.Orders;
using VarinaCmsV2.ViewModel.Eshop.Discount;

namespace VarinaCmsV2.ViewModel.Eshop.Orders
{
    public class OrderLiquidViewModel: LiquidAdapter
    {
        public DateTime UpdateDateTime { get; set; }
        public DateTime CreateDateTime { get; set; }
        public Guid CreatorId { get; set; }
        public long OrderTrackNumber { get; set; }
        public DateTime? PaidDate { get; set; }
        public bool IsDeleted { get; set; }
        public OrderStatus OrderStatus { get; set; }

        public ICollection<DiscountUsageHistoryLiquidViewModel> DiscountUsageHistories { get; set; }
        public ICollection<OrderItemLiquidViewModel> OrderItems { get; set; }
    }
}
