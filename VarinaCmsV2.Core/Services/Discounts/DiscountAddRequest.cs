using System.Security.Principal;
using VarinaCmsV2.ViewModel.Entities;
using VarinaCmsV2.ViewModel.Eshop.Discount;

namespace VarinaCmsV2.Core.Services.Discounts
{
    public class DiscountAddRequest
    {
        public IPrincipal RequestOwner { get; set; }
        public DiscountAddOrUpdateModel ViewModel { get; set; }
    }
}