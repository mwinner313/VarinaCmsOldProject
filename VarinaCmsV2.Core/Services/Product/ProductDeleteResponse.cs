using System.Net.Http.Headers;

namespace VarinaCmsV2.Core.Services.Product
{
    public class ProductDeleteResponse
    {
        public ResponseAccess Access { get; set; }
        public string Message { get; set; } = "";
    }
}