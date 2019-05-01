using Newtonsoft.Json;

namespace VarinaCmsV2.DomainClasses.Users
{
    public class Premission
    {
        [JsonProperty("action")]
        public string Action { get; set; }

        public bool IsSystematic { get; set; }
    }
}
