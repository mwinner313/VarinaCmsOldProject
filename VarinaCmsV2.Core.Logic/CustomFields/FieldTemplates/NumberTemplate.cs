using Newtonsoft.Json;

namespace VarinaCmsV2.Core.Logic.CustomFields.FieldTemplates
{
    public class NumberTemplate
    {
        public NumberTemplate()
        {
        }

        [JsonProperty("number")]
        public string Number { get; set; }
        [JsonProperty("numberType")]
        public string NumberType { get; set; }
    }
}