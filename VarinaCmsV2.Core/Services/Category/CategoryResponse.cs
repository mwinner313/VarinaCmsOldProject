using VarinaCmsV2.ViewModel.Category;

namespace VarinaCmsV2.Core.Services.Category
{
    public class CategoryResponse:IServiceResponse
    {
        public ResponseAccess Access { get; set; }
        public string Message { get; set; } = "";
        public CategoryViewModel Category { get; set; }
    }
}