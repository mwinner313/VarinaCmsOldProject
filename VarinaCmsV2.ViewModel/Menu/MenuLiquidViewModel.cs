using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.Common;
using VarinaCmsV2.DomainClasses.Contracts;

namespace VarinaCmsV2.ViewModel.Menu
{
    public class MenuLiquidViewModel : LiquidAdapter
    {
        public string Title { get; set; }
        public List<MenuItemLiquidViewModel> MenuItems { get; set; }
    }
}
