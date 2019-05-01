using System.Security.Principal;
using VarinaCmsV2.ViewModel.Category;

namespace VarinaCmsV2.Core.Services.Category
{
    public class CategoryAddRequest
    {
        public CategoryAddOrUpdateViewModel Model { get; set; }
        public IPrincipal RequestOwner { get; set; }

    }
}