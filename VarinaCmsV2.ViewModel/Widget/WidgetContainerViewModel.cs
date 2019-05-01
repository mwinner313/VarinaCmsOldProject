using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VarinaCmsV2.ViewModel.Widget
{
    public class WidgetContainerViewModel:BaseVeiwModel
    {
        public string Title { get; set; }
        public string Handle { get; set; }
        public List<WidgetViewModel> Widgets { get; set; }
    }
}
