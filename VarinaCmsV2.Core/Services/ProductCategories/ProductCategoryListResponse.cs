using System.Collections.Generic;
using VarinaCmsV2.ViewModel.Eshop;

namespace VarinaCmsV2.Core.Services.ProductCategories
{
    public class ProductCategoryListResponse : IServiceResponse
    {
        public ResponseAccess Access { get; set; }
        public string Message { get; set; }
        public IList<ProductCategoryViewModel> Categories { get; set; }
        public int Count { get; set; }
    }
}