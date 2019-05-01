using System;

namespace VarinaCmsV2.ViewModel.Eshop.Discount
{
    public class DiscountUsageHistoryLiquidViewModel
    {
        public Guid DiscountId { get; set; }
        public Guid OrderId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public string DiscountTitle { get; set; }
    }
}