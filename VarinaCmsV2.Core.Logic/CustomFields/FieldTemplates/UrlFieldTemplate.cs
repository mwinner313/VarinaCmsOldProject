using Newtonsoft.Json;

namespace VarinaCmsV2.Core.Logic.CustomFields.FieldTemplates
{
    public class UrlFieldTemplate
    {
        [JsonProperty("url")]
        public string Url { get; set; }
    }
}