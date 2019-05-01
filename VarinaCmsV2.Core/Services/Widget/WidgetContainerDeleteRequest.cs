using System;
using System.Security.Principal;

namespace VarinaCmsV2.Core.Services.Widget
{
    public class WidgetContainerDeleteRequest:IServiceRequest
    {
        public Guid WidgetContainerId { get; set; }
        public IPrincipal RequestOwner { get; set; }
    }

}