using System;
using Newtonsoft.Json;

namespace VarinaCmsV2.Core.Logic.Widgets.Menu
{
    public class MenuMetaData
    {
        [JsonProperty("menuId")]
        public Guid MenuId { get; set; }
    }
}