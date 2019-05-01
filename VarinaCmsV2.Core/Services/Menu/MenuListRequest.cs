using System.Security.Principal;

namespace VarinaCmsV2.Core.Services.Menu
{
    public class MenuListRequest:IServiceRequest
    {
        public IPrincipal RequestOwner { get; set; }
    }
}