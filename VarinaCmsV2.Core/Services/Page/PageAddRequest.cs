using System.Security.Principal;
using VarinaCmsV2.ViewModel.Page;

namespace VarinaCmsV2.Core.Services.Page
{
    public class PageAddRequest
    {
        public IPrincipal RequestOwner { get; set; }
        public PageAddUpdateViewModel ViewModel { get; set; }
    }
}