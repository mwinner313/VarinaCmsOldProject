using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VarinaCmsV2.Core.Services.Page
{
    public interface IPageService
    {
        Task<PageResponse >Get(PageRequest request);
        Task<PageListResponse> Get(PageListRequest request);
        Task<PageResponse >Add(PageAddRequest request);
        Task<PageResponse> Update(PageUpdateRequest request);
        Task<PageResponse> Delete(PageDeleteRequest request);
    }
}
