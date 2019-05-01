using System;
using System.Security.Principal;
using VarinaCmsV2.ViewModel.Eshop;

namespace VarinaCmsV2.Core.Services.ProductCategories
{
    public class ProductCategoryEditRequest
    {
        public IPrincipal RequestOwner { get; set; }
        public ProductCategoryAddOrUpdateModel Model { get; set; }
        public Guid Id { get; set; }
    }
}