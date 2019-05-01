using System;
using System.Security.Principal;
using VarinaCmsV2.ViewModel.Menu;

namespace VarinaCmsV2.Core.Services.Menu
{
    public class MenuUpdateRequest:IServiceRequest
    {
        public IPrincipal RequestOwner { get; set; }
        public MenuAddUpdateViewModel ViewModel { get; set; }
        public Guid MenuId { get; set; }
    }
}