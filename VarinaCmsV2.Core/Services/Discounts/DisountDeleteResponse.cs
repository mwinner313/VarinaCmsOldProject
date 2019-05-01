using System.Net.Http.Headers;

namespace VarinaCmsV2.Core.Services.Discounts
{
    public class DisountDeleteResponse
    {
        public ResponseAccess Access { get; set; }
        public string Message { get; set; } = "";
    }
}