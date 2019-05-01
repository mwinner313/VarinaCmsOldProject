using Newtonsoft.Json;

namespace VarinaCmsV2.Core.Logic.CustomFields.FieldTemplates
{
   
    public class TextTemplate
    {
        [JsonProperty("text")]
        public string Text { get; set; }
    }
}
