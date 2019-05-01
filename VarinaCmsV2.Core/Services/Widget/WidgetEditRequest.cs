using System;
using System.Security.Principal;
using VarinaCmsV2.ViewModel.Widget;

namespace VarinaCmsV2.Core.Services.Widget
{
    public class WidgetEditRequest
    {
        public WidgetAddOrUpdateViewModel Model { get; set; }
        public IPrincipal RequestOwner { get; set; }
        public Guid WidgetId { get; set; }
    }
}