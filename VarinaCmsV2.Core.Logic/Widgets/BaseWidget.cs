using Newtonsoft.Json.Linq;
using StructureMap;
using System;
using VarinaCmsV2.Core.Contracts.Widget;

namespace VarinaCmsV2.Core.Logic.Widgets
{
    public abstract class BaseWidget :  IWidget
    {
        protected readonly IContainer Container;
        public static Func<IContainer> GetContainer { get; set; }
        protected BaseWidget()
        {
            Container = GetContainer();
        }

        public abstract string Type { get; }
        public abstract BaseWidgetLiquidData Parse(JObject widgetMetaData);
    }
}
