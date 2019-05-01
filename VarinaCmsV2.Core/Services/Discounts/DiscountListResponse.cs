using System.Collections.Generic;
using VarinaCmsV2.ViewModel.Eshop.Discount;

namespace VarinaCmsV2.Core.Services.Discounts
{
    public class DiscountListResponse : IServiceResponse
    {
        public List<DiscountViewModel> Items { get; set; }
        public ResponseAccess Access { get; set; }
        public string Message { get; set; }
        public long Count { get; set; }
    }
}