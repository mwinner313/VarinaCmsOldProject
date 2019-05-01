using Newtonsoft.Json;

namespace VarinaCmsV2.Core.Logic.CustomFields.FieldTemplates
{
    public class HtmlFieldTemplate
    {
        [JsonProperty("content")]
        public string Content { get; set; }
    }
}