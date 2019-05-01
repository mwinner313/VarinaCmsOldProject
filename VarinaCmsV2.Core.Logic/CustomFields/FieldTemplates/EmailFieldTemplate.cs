using Newtonsoft.Json;

namespace VarinaCmsV2.Core.Logic.CustomFields.FieldTemplates
{
    internal class EmailFieldTemplate
    {
        [JsonProperty("emailAddress")]
        public string EmailAddress { get; set; }
    }
}