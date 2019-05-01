using System.Collections.Generic;
using VarinaCmsV2.ViewModel.Menu;

namespace VarinaCmsV2.Core.Services.Menu
{
    public class MenuListResponse:IServiceResponse
    {
        public List<MenuViewModel> Menus { get; set; }
        public ResponseAccess Access { get; set; }
        public string Message { get; set; }
       
    }
}