using System.Net.Http.Headers;

namespace VarinaCmsV2.Core.Services.Discounts
{
    public class DiscountAddResponse
    {
        public ResponseAccess Access { get; set; }
        public string Message { get; set; } = "";
    }
}