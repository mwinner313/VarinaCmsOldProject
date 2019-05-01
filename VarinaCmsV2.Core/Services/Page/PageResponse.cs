using VarinaCmsV2.ViewModel.Page;

namespace VarinaCmsV2.Core.Services.Page
{
    public class PageResponse:IServiceResponse
    {
        public PageViewModel Page { get; set; }
        public ResponseAccess Access { get; set; }
        public string Message { get; set; } = "";
    }
}