using System.Security.Principal;
using VarinaCmsV2.ViewModel.Eshop;

namespace VarinaCmsV2.Core.Services.Product
{
    public class ProductAddRequest
    {
        public IPrincipal RequestOwner { get; set; }
        public ProductAddOrUpdateModel ProductToAdd { get; set; }
    }
}