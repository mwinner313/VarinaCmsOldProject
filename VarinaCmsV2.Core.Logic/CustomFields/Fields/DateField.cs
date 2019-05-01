using Newtonsoft.Json.Linq;
using VarinaCmsV2.Common.CustomFields;
using VarinaCmsV2.Core.Logic.CustomFields.FieldTemplates;

namespace VarinaCmsV2.Core.Logic.CustomFields.Fields
{
    public class DateField:CustomField<JObject>
    {
        public DateField(CustomFieldFactory<JObject> factory, string fieldName, JObject value) : base(factory, fieldName, value)
        {
        }

        public DateField(CustomFieldFactory<JObject> factory, string fieldName) : base(factory, fieldName)
        {
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public override object ToLiquid()
        {
            return Value.ToObject<DateTimeAndDateTemplate>().DateTime;
        }

        public override string Veiw()
        {
            return Value.ToObject<DateTimeAndDateTemplate>()?.DateTime.ToString("dddd dd MMMM yyy");
        }
    }
}
