using System.Threading.Tasks;

namespace VarinaCmsV2.Core.Services.Category
{
    public interface ICategoryService
    {
        Task<CategoryListResponse> Get(CategoryListRequest request);
        Task<CategoryResponse> Add(CategoryAddRequest request);
        Task<CategoryResponse> Update(CategoryUpdateRequest request);
        Task<CategoryResponse> Delete(CategoryDeleteRequest request);
        Task<SimpleResonse> UpdateParentListRequest(CategoryParentChildUpdateRequest request);
    }
}
