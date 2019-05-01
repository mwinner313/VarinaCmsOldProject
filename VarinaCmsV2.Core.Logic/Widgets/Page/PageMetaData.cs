using System;
using Newtonsoft.Json;

namespace VarinaCmsV2.Core.Logic.Widgets.Page
{
    public class PageMetaData
    {
        [JsonProperty("pageId")]
        public Guid PageId { get; set; }
    }
}