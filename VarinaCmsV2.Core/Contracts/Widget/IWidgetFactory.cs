using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VarinaCmsV2.Core.Contracts.Widget
{
    public interface IWidgetFactory
    {
        IWidget Get(string type);
        List<AvailibleWidget> GetAvailibleWidgets();
    }
}
