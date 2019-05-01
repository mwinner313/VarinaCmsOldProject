using System.Collections.Generic;
using DotLiquid;
using Newtonsoft.Json.Linq;
using VarinaCmsV2.Core.Contracts.Widget;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.ViewModel.Entities;

namespace VarinaCmsV2.Core.Logic.Widgets
{
    internal class ProductWidget : IWidget
    {
        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public string Type { get; } = "product";
        public BaseWidgetLiquidData Parse(JObject widgetMetaData)
        {
            throw new System.NotImplementedException();
        }
    }
}