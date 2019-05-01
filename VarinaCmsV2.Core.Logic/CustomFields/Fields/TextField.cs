using Newtonsoft.Json.Linq;
using VarinaCmsV2.Common.CustomFields;
using VarinaCmsV2.Core.Logic.CustomFields.FieldTemplates;

namespace VarinaCmsV2.Core.Logic.CustomFields.Fields
{
    public class TextField : CustomField<JObject>
    {
        public TextField(CustomFieldFactory<JObject> factory, string fieldName) : base(factory, fieldName)
        {
        }
        public override string ToString()
        {
            return Value.ToString();
        }

        public override object ToLiquid()
        {
            return Value.ToObject<TextTemplate>().Text.Trim();
        }

        public override string Veiw()
        {
            return Value.ToObject<TextTemplate>()?.Text?.Trim();
        }
    }
}
