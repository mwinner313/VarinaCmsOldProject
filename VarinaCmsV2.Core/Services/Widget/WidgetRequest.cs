using System.Security.Principal;

namespace VarinaCmsV2.Core.Services.Widget
{
    public class WidgetContainerGetListRequest:IServiceRequest
    {
        public IPrincipal RequestOwner { get; set; }
    }
}