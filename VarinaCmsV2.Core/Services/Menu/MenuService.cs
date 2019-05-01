using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.ViewModel.Menu;

namespace VarinaCmsV2.Core.Services.Menu
{
    public interface IMenuService
    {
        Task<MenuListResponse> Get(MenuListRequest request);
        Task<MenuResponse> Add(MenuAddRequest request);
        Task<MenuResponse> Update(MenuUpdateRequest request);
        Task<MenuResponse> Delete(MenuDeleteRequest request);
        Task<List<MenuItemViewModel>> AddSingleMenuItem(List<MenuItemAddUpdateViewModel> model);
    }
}
