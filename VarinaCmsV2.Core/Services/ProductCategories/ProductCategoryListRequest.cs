using System.Security.Principal;
using VarinaCmsV2.Common;

namespace VarinaCmsV2.Core.Services.ProductCategories
{
    public class ProductCategoryListRequest
    {
        public ProductCategoryQuery Query { get; set; }
        public IPrincipal RequestOwner { get; set; }
    }
}