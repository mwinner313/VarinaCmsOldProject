using Newtonsoft.Json.Linq;
using VarinaCmsV2.Common.CustomFields;
using VarinaCmsV2.Core.Logic.CustomFields.Fields;
using VarinaCmsV2.Core.Logic.CustomFields.FieldTemplates;

namespace VarinaCmsV2.Core.Logic.CustomFields.Factories
{
    public class TextFieldFactory : CustomFieldFactory<JObject>
    {

        public TextFieldFactory() : base(fieldType: "text")
        {
        }

        public override bool IsValidForField(object data, JObject fdMetaData = null) => data is string;

        public override CustomField<JObject> CreateNew(string fieldName, object data, JObject fdMetaData)
        {
            var customField = new TextField(this, fieldName)
            { Value = JObject.FromObject(new TextTemplate() { Text = data.ToString() }) };
            return customField;
        }

        public override CustomField<JObject> LoadField(JObject data, string fieldName, JObject metaData)
        {
            return new TextField(this, fieldName) { Value = data };
        }

        public override bool IsValidForField(JObject data, JObject metaData = null)
        {
            return data.ToObject<TextTemplate>()?.Text != null;
        }
    }
}
