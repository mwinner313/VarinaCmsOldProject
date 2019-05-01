using VarinaCmsV2.DomainClasses.Entities.EShop;

namespace VarinaCmsV2.Core.Contracts.WebClientServices.Products
{
    public class DownloadProductResponse
    {
        public DownloadResponseStatus Status { get; set; }
        public Product Product { get; set; }
    }
}