using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Build.Framework;

namespace VarinaCmsV2.ViewModel.Menu
{
    public class MenuAddUpdateViewModel:BaseVeiwModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string LanguageName { get; set; }
        public List<MenuItemAddUpdateViewModel> MenuItems { get; set; }
    }
}
