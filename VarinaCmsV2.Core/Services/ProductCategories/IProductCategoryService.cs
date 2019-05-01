using System.Threading.Tasks;

namespace VarinaCmsV2.Core.Services.ProductCategories
{
    public interface IProductCategoryService
    {
        Task<ProductCategoryListResponse> Get(ProductCategoryListRequest request);
        Task<ProductCategoryAddResponse> Add(ProductCategoryAddRequest request);
        Task<ProductCategoryEditResponse> Edit(ProductCategoryEditRequest request);
        Task<ProductCategoryDeleteResponse> Delete(ProductCategoryDeleteRequest request);
        Task<SimpleResonse> UpdateParentListRequest(ParentChildUpdateRequest request);
    }
}
