using System;
using System.Security.Principal;

namespace VarinaCmsV2.Core.Services.Widget
{
    public class WidgetDeleteRequest:IServiceRequest
    {
        public Guid WidgetId { get; set; }
        public IPrincipal RequestOwner { get; set; }
    }
}