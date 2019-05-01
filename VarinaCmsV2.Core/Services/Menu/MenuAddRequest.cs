using System.Security.Principal;
using VarinaCmsV2.ViewModel.Menu;

namespace VarinaCmsV2.Core.Services.Menu
{
    public class MenuAddRequest:IServiceRequest
    {
        public IPrincipal RequestOwner { get; set; }
        public MenuAddUpdateViewModel ViewModel { get; set; }
    }
}