using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VarinaCmsV2.Core.Services.Widget
{
    public interface IWidgetContainerService
    {
        Task<WidgetContainerListResponse> Get(WidgetContainerGetListRequest listRequest);
        Task<WidgetContainerResponse> Add(WidgetContainerAddRequest request);
        Task<WidgetContainerResponse> Edit(WidgetContainerEditRequest request);
        Task<WidgetContainerResponse> Delete(WidgetContainerDeleteRequest request);
        // widgets
        Task<WidgetResponse> AddWidget(WidgetAddRequest request);
        Task<WidgetResponse> EditWidget(WidgetEditRequest request);
        Task<WidgetResponse> DeleteWidget(WidgetDeleteRequest request);
    }
}
