using System;
using System.Security.Principal;
using VarinaCmsV2.ViewModel.Eshop;

namespace VarinaCmsV2.Core.Services.Product
{
    public class ProductEditRequest
    {
        public Guid Id { get; set; }
        public ProductAddOrUpdateModel Model { get; set; }
        public IPrincipal RequestOwner { get; set; }
    }
}