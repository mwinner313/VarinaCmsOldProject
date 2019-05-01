using System;
using System.Security.Principal;
using VarinaCmsV2.ViewModel.Widget;

namespace VarinaCmsV2.Core.Services.Widget
{
    public class WidgetContainerEditRequest:IServiceRequest
    {
        public Guid WidgetContainerId { get; set; }
        public WidgetContainerAddOrUpdateViewModel Model { get; set; }
        public IPrincipal RequestOwner { get; set; }
    }
}