using System.Collections.Generic;
using VarinaCmsV2.Core.Contracts.Widget;
using VarinaCmsV2.ViewModel.Widget;

namespace VarinaCmsV2.Core.Services.Widget
{
    public class WidgetContainerListResponse : IServiceResponse
    {
        public List<AvailibleWidget> AvailibleWidgets { get; set; }
        public List<WidgetContainerViewModel> WidgetContainers { get; set; }
        public ResponseAccess Access { get; set; }
        public string Message { get; set; }
    }
}