using System.Security.Principal;
using VarinaCmsV2.ViewModel.Widget;

namespace VarinaCmsV2.Core.Services.Widget
{
    public class WidgetAddRequest:IServiceRequest
    {
        public WidgetAddOrUpdateViewModel Model { get; set; }
        public IPrincipal RequestOwner { get; set; }
    }
}