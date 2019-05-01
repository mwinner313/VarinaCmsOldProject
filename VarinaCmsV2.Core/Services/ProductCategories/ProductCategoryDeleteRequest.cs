using System;
using System.Security.Principal;

namespace VarinaCmsV2.Core.Services.ProductCategories
{
    public class ProductCategoryDeleteRequest
    {
        public IPrincipal RequestOwner { get; set; }
        public Guid Id { get; set; }
    }
}