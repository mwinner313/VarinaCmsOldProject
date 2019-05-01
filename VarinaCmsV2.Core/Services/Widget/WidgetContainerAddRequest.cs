using System.Security.Principal;
using VarinaCmsV2.ViewModel.Widget;

namespace VarinaCmsV2.Core.Services.Widget
{
    public class WidgetContainerAddRequest:IServiceRequest
    {
        public WidgetContainerAddOrUpdateViewModel Model { get; set; }
        public IPrincipal RequestOwner { get; set; }
    }
}