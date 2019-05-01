using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotLiquid;

namespace VarinaCmsV2.ViewModel.Settings
{
    public class WebSiteBasicInformationLiquidVeiwModel:Drop
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string MetaTags { get; set; }
        public string Instagram { get; set; }
        public string Facebook { get; set; }
        public string LinkedIn { get; set; }
        public string GooglePlus { get; set; }
        public string Telegram { get; set; }
        public string Namasha { get; set; }
        public string Aparat { get; set; }
        public string Address { get; set; }
        public string GoogleMapLatLang { get; set; }
        public string GoogleAnalytics { get; set; }
    }
}
