using VarinaCmsV2.ViewModel.Eshop;

namespace VarinaCmsV2.Core.Services.ProductCategories
{
    public class ProductCategoryEditResponse
    {
        public ResponseAccess Access { get; set; }
        public ProductCategoryViewModel Category { get; set; }
    }
}