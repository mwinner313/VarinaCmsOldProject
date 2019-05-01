using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VarinaCmsV2.ViewModel.Widget
{
    public class WidgetContainerAddOrUpdateViewModel:BaseVeiwModel
    {
        public string Title { get; set; }
        public string Handle { get; set; }
        public List<WidgetAddOrUpdateViewModel> Widgets { get; set; }
    }
}
