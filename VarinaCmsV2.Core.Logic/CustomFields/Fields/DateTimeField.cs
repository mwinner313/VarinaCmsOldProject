using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using VarinaCmsV2.Common.CustomFields;
using VarinaCmsV2.Core.Logic.CustomFields.FieldTemplates;
using VarinaCmsV2.Common;

namespace VarinaCmsV2.Core.Logic.CustomFields.Fields
{
    public class DateTimeField:CustomField<JObject>
    {
        public DateTimeField(CustomFieldFactory<JObject> factory, string fieldName, JObject value) : base(factory, fieldName, value)
        {
        }

        public DateTimeField(CustomFieldFactory<JObject> factory, string fieldName) : base(factory, fieldName)
        {
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public override object ToLiquid()
        {
            return AutoMapper.Mapper.Map<LiquidDateTime>(Value.ToObject<DateTimeAndDateTemplate>().DateTime);
        }

        public override string Veiw()
        {
            return Value.ToObject<DateTimeAndDateTemplate>()?.DateTime.ToString("dddd dd MMMM yyy | HH:mm");
        }
    }
}
