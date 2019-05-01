using VarinaCmsV2.Core.Services;
using VarinaCmsV2.DomainClasses.Entities.EShop;
using VarinaCmsV2.ViewModel.Eshop.Review;

namespace VarinaCmsV2.Core.Contracts.WebClientServices.Products
{
    public class AddCustomerReviewResponse
    {
        public ResponseAccess Access { get; set; }
        public ReviewViewModel Review { get; set; }
        public string Message { get; set; }
        public Product Product { get; set; }
    }
}