using DotLiquid;
using Newtonsoft.Json;

namespace VarinaCmsV2.Core.Logic.CustomFields.FieldTemplates
{
    public class FileFieldTemplate:Drop
    {
        [JsonProperty("path")]
        public string Path { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("extention")]
        public string Extention { get; set; }
    }
}