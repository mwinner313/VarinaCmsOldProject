using DotLiquid;
using Newtonsoft.Json.Linq;

namespace VarinaCmsV2.Core.Contracts.Widget
{
    public interface IWidget
    {
        string Type { get; }
        BaseWidgetLiquidData Parse(JObject widgetMetaData);
    }
}
