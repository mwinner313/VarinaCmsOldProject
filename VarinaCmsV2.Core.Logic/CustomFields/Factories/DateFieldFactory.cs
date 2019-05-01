using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using VarinaCmsV2.Common;
using VarinaCmsV2.Common.CustomFields;
using VarinaCmsV2.Core.Logic.CustomFields.Fields;
using VarinaCmsV2.Core.Logic.CustomFields.FieldTemplates;

namespace VarinaCmsV2.Core.Logic.CustomFields.Factories
{
    public class DateFieldFactory : CustomFieldFactory<JObject>
    {
        public DateFieldFactory() : base("date")
        {
        }

        public override bool IsValidForField(object data, JObject fdMetaData = null)
        {
            return (data is string str) && Regex.IsMatch(str, RegexPattern.Date);
        }

        public override bool IsValidForField(JObject data, JObject metaData)
        {
            var datetimefield = data?.ToObject<DateTimeAndDateTemplate>();
            if (datetimefield?.DateTimeString == null) return false;
            if (datetimefield.DateTimeString == "") return true;
            return Regex.IsMatch(datetimefield.DateTimeString, RegexPattern.Date);
        }

        public override CustomField<JObject> CreateNew(string fieldName, object data, JObject fdMetaData)
        {
            if (!IsValidForField(data)) throw new InvalidOperationException($"invalid data for date field {data}");
            return new DateField(this, fieldName, JObject.FromObject(new DateTimeAndDateTemplate() {DateTime = DateHelper.ParseSafe(data as string) }));
        }

        public override CustomField<JObject> LoadField(JObject data, string fieldName, JObject metaData)
        {
          if(!IsValidForField(data))throw new InvalidOperationException($"invalid data for field : {fieldName} => {data}");
          return new DateField(this,fieldName,data);
        }
    }
}
