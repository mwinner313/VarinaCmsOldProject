using DotLiquid;
using Newtonsoft.Json.Linq;
using VarinaCmsV2.Core.Contracts.Widget;
using VarinaCmsV2.DomainClasses.Contracts;

namespace VarinaCmsV2.Core.Logic.Widgets
{
    internal class CalendarWidget : IWidget
    {
        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public string Type { get; } = "calendar";
        public BaseWidgetLiquidData Parse(JObject widgetMetaData)
        {
            throw new System.NotImplementedException();
        }
    }
}