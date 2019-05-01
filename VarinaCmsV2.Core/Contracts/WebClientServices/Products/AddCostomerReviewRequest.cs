using System.Security.Principal;
using VarinaCmsV2.ViewModel.Eshop.Review;

namespace VarinaCmsV2.Core.Contracts.WebClientServices.Products
{
    public class AddCostomerReviewRequest
    {
        public ReviewAddOrUpdateViewModel Model { get; set; }
        public IPrincipal RequestOwner { get; set; }
    }
}