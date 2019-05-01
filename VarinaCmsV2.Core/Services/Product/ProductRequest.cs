using System;
using System.Security.Principal;

namespace VarinaCmsV2.Core.Services.Product
{
    public class ProductRequest
    {
        public Guid ProductId { get; set; }
        public IPrincipal RequestOwner { get; set; }
    }
}