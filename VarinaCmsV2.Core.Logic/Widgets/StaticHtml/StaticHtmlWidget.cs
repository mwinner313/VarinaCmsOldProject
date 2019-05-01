using DotLiquid;
using Newtonsoft.Json.Linq;
using VarinaCmsV2.Core.Contracts.Widget;

namespace VarinaCmsV2.Core.Logic.Widgets.StaticHtml
{
    internal class StaticHtmlWidget :BaseWidgetWithMetaData<StaticHtml>
    {
        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public override string Type { get; } = "html";
    

        public override BaseWidgetLiquidData Parse(JObject widgetMetaData)
        {
          base.DeserializeMetaData(widgetMetaData);
            return MetaData;
        }
    }
}