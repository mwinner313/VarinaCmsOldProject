using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VarinaCmsV2.DomainClasses.Entities.EShop;
using VarinaCmsV2.ViewModel.Eshop;

namespace VarinaCmsV2.Core.Contracts.WebClientServices.Products
{
    public interface IProductWebClientService
    {
        Task<Product> GetByUrlAsync(string productUrl);
        Task<PaginatedProductCategory> ListByCategoryAsync(string productCategoryUrl, int pageNumber);
        Task<SearchProductResult> Search(string searchText);
        Task<AddCustomerReviewResponse> AddCustomerReview(AddCostomerReviewRequest model);
        Task<PaginatedProducTag> ListByTagAsync(string tagUrl, int pageNumber);
        Task<ProductsCompareModel> GetProductCompareViewData(params string[] products);
        Task<DownloadProductResponse> GetProductForDownLoad(Guid orderId, Guid productId);
        Task<DownloadSampleProductResponse> GetProductSampleForDownLoad(Guid productId);
    }

   
}
