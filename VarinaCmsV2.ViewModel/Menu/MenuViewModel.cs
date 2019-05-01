using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.Common;

namespace VarinaCmsV2.ViewModel.Menu
{
    public class MenuViewModel:BaseVeiwModel
    {
        public string Title { get; set; }
        public string LanguageName { get; set; }
        public List<MenuItemViewModel> MenuItems { get; set; }
    }
}
