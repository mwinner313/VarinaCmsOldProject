using VarinaCmsV2.ViewModel.Eshop;

namespace VarinaCmsV2.Core.Services.Product
{
    public class ProductResponse : IServiceResponse
    {
        public ResponseAccess Access { get; set; }
        public string Message { get; set; }
        public ProductViewModel Product { get; set; }
    }
}