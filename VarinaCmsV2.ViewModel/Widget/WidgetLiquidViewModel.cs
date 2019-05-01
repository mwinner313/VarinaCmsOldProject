using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using VarinaCmsV2.DomainClasses.Contracts;

namespace VarinaCmsV2.ViewModel.Widget
{
    public class WidgetLiquidViewModel:LiquidAdapter
    {
        public string Type { get; set; }
        public string Title { get; set; }
        public JObject MetaData { get; set; }
    }
}
