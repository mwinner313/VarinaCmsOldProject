using System;
using System.Security.Principal;

namespace VarinaCmsV2.Core.Services.Discounts
{
    public class DiscountRequest
    {
        public Guid Id { get; set; }
        public IPrincipal RequestOwner { get; set; }    
    }
}