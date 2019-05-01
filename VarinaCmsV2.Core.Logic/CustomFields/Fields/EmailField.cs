using Newtonsoft.Json.Linq;
using VarinaCmsV2.Common.CustomFields;
using VarinaCmsV2.Core.Logic.CustomFields.Factories;
using VarinaCmsV2.Core.Logic.CustomFields.FieldTemplates;

namespace VarinaCmsV2.Core.Logic.CustomFields.Fields
{
    internal class EmailField : CustomField<JObject>
    {


        public EmailField(CustomFieldFactory<JObject> emailFieldFactory, string fieldName, JObject data) : base(emailFieldFactory, fieldName, data)
        {

        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public override object ToLiquid()
        {
            return Value.ToObject<EmailFieldTemplate>().EmailAddress;
        }

        public override string Veiw()
        {
            return Value.ToObject<EmailFieldTemplate>()?.EmailAddress;
        }
    }
}