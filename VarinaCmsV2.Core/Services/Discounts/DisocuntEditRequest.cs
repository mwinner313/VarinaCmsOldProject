using System;
using System.Security.Principal;
using VarinaCmsV2.ViewModel.Eshop.Discount;

namespace VarinaCmsV2.Core.Services.Discounts
{
    public class DisocuntEditRequest
    {
        public IPrincipal RequestOwner { get; set; }
        public DiscountAddOrUpdateModel Model { get; set; }
        public Guid DiscountId { get; set; }
    }
}