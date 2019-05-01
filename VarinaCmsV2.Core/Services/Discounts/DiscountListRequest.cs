using System.Security.Principal;

namespace VarinaCmsV2.Core.Services.Discounts
{
    public class DiscountListRequest
    {
        public DiscountQuery Query { get; set; }
        public IPrincipal RequestOwner { get; set; }


    }
}