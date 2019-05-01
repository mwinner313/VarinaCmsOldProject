using System.Net.Http.Headers;
using VarinaCmsV2.ViewModel.Eshop.Discount;

namespace VarinaCmsV2.Core.Services.Discounts
{
    public class DisountEditResponse
    {
        public DiscountViewModel Model { get; set; }
        public ResponseAccess Access { get; set; }
        public string Message { get; set; } = "";
    }
}