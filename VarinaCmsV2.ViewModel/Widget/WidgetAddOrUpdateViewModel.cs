using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace VarinaCmsV2.ViewModel.Widget
{
    public class WidgetAddOrUpdateViewModel:BaseVeiwModel
    {
        public string Type { get; set; }
        public Guid ContainerId { get; set; }
        public int Order { get; set; }
        public string LanguageName { get; set; }
        public string Title { get; set; }
        public string Handle { get; set; }
        public JObject MetaData { get; set; }
    }
}
