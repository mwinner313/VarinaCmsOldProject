using System;
using System.Security.Principal;
using VarinaCmsV2.ViewModel.Category;

namespace VarinaCmsV2.Core.Services.Category
{
    public class CategoryUpdateRequest:IServiceRequest
    {
        public CategoryAddOrUpdateViewModel ViewModel { get; set; }
        public Guid Id { get; set; }
        public IPrincipal RequestOwner { get; set; }
    }
}