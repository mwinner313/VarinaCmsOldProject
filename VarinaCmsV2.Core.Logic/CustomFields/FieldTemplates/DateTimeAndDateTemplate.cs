using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace VarinaCmsV2.Core.Logic.CustomFields.FieldTemplates
{
    public class DateTimeAndDateTemplate
    {
        [JsonProperty("dateTime")]
        public DateTime DateTime { get; set; }
        [JsonProperty("dateTimeString")]
        public string DateTimeString { get; set; }
    }
}
