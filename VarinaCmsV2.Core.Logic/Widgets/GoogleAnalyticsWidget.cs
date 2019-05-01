using DotLiquid;
using Newtonsoft.Json.Linq;
using VarinaCmsV2.Core.Contracts.Widget;
using VarinaCmsV2.DomainClasses.Contracts;

namespace VarinaCmsV2.Core.Logic.Widgets
{
    public class GoogleAnalyticsWidget : IWidget
    {
       
        public string Type { get; } = "google-analytics";
    
        public BaseWidgetLiquidData Parse(JObject widgetMetaData)
        {
            throw new System.NotImplementedException();
        }
    }
}