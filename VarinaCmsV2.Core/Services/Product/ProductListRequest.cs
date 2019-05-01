using System.Security.Principal;

namespace VarinaCmsV2.Core.Services.Product
{
    public class ProductListRequest
    {
        public ProductQuery Query { get; set; }
        public IPrincipal RequestOwner { get; set; }
    }
}