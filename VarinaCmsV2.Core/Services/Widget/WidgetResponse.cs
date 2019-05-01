using VarinaCmsV2.ViewModel.Widget;

namespace VarinaCmsV2.Core.Services.Widget
{
    public class WidgetResponse:IServiceResponse
    {
        public WidgetViewModel Model { get; set; }
        public ResponseAccess Access { get; set; }
        public string Message { get; set; }
    }
}