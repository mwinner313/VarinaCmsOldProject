using System.Threading.Tasks;

namespace VarinaCmsV2.Core.Services.Product
{
    public interface IProductService
    {
        Task<ProductListResponse> Get(ProductListRequest request);
        Task<ProductResponse> Get(ProductRequest request);
        Task<ProductAddResponse> Add(ProductAddRequest request);
        Task<ProductEditResponse> Edit(ProductEditRequest request);
        Task<ProductDeleteResponse> Delete(ProductDeleteRequest request);
    }
}
