using VarinaCmsV2.ViewModel.Menu;

namespace VarinaCmsV2.Core.Services.Menu
{
    public class MenuResponse:IServiceResponse
    {
        public ResponseAccess Access { get; set; }
        public string Message { get; set; }
        public MenuViewModel Menu { get; set; }
    }
}