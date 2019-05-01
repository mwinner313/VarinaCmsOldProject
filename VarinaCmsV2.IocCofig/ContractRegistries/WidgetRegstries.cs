using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StructureMap;
using StructureMap.Web;
using VarinaCmsV2.Core.Contracts.Widget;
using VarinaCmsV2.Core.Logic.Decorators;
using VarinaCmsV2.Core.Logic.Widgets;

namespace VarinaCmsV2.IocCofig.ContractRegistries
{
    public class WidgetRegstries : Registry
    {
        public WidgetRegstries()
        {
            BaseWidget.GetContainer = AppObjectFactory.GetContainer;
            LiquidWidgetFinder.GetContainer = AppObjectFactory.GetContainer;
            For<IWidgetFactory>().Singleton().Use<WidgetFactory>();
        }
    }
}