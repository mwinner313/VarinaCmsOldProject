using System;
using System.Security.Principal;

namespace VarinaCmsV2.Core.Services.Menu
{
    public class MenuDeleteRequest:IServiceRequest
    {
        public IPrincipal RequestOwner { get; set; }
        public Guid MenuId { get; set; }
    }
}