using System.Security.Principal;
using VarinaCmsV2.ViewModel.Eshop;

namespace VarinaCmsV2.Core.Services.ProductCategories
{
    public class ProductCategoryAddRequest
    {
        public ProductCategoryAddOrUpdateModel Model { get; set; }
        public IPrincipal RequestOwner { get; set; }
    }
}