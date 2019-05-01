using System.Net.Http.Headers;
using VarinaCmsV2.ViewModel.Eshop;

namespace VarinaCmsV2.Core.Services.Product
{
    public class ProductEditResponse
    {
        public ResponseAccess Access { get; set; }
        public ProductViewModel Updated { get; set; }
        public string Message { get; set; } = "";
    }
}