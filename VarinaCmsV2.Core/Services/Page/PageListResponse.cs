using System.Collections.Generic;
using System.Security.Principal;
using VarinaCmsV2.ViewModel.Page;

namespace VarinaCmsV2.Core.Services.Page
{
    public class PageListResponse: IServiceResponse
    {
        public ResponseAccess Access { get; set; }
        public string Message { get; set; }
        public List<PageViewModel> Pages { get; set; }
        public long Count { get; set; }
    }
}