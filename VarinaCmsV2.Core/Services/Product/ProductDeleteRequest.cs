using System;
using System.Security.Principal;

namespace VarinaCmsV2.Core.Services.Product
{
    public class ProductDeleteRequest
    {
        public IPrincipal RequestOwner { get; set; }
        public Guid Id { get; set; }
    }
}