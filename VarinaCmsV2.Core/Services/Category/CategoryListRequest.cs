using System.Security.Principal;
using VarinaCmsV2.Common;

namespace VarinaCmsV2.Core.Services.Category
{
    public class CategoryListRequest
    {
        public CategoryQuery Query { get; set; }
        public IPrincipal RequestOwner { get; set; }
    }
}