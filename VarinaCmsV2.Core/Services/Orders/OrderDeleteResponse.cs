using System.Net.Http.Headers;

namespace VarinaCmsV2.Core.Services.Orders
{
    public class OrderDeleteResponse
    {
        public ResponseAccess Access { get; set; }
        public string Message { get; set; } = "";
    }
}