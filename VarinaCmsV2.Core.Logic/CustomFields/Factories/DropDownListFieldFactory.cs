using System;
using System.Linq;
using Newtonsoft.Json.Linq;
using VarinaCmsV2.Common.CustomFields;
using VarinaCmsV2.Core.Logic.CustomFields.Fields;
using VarinaCmsV2.Core.Logic.CustomFields.FieldTemplates;
using VarinaCmsV2.Core.Logic.CustomFields.MetaDataTemplate;

namespace VarinaCmsV2.Core.Logic.CustomFields.Factories
{
    public class DropDownListFieldFactory : CustomFieldFactory<JObject>
    {
        public DropDownListFieldFactory() : base("dropdown")
        {
        }

        public override bool IsValidForField(object data, JObject fdMetaData = null)
        {
            var fieldOptions = GetFieldOptions(fdMetaData);
            return data != null && fieldOptions.AvalibleOptions.Any(x => x.Value == data.ToString());
        }


        public override bool IsValidForField(JObject data, JObject metaData)
        {
            var fieldOptions = GetFieldOptions(metaData);

            var selectedOption = data?.ToObject<DropDownListFieldTemplate>() ??
                                 throw new InvalidOperationException("data is null");

            return fieldOptions.AvalibleOptions.Any(x =>
                x.Value == selectedOption.Value && x.Name == selectedOption.Name);
        }

        public override CustomField<JObject> CreateNew(string fieldName, object data, JObject fdMetaData)
        {
            var fieldOptions = GetFieldOptions(fdMetaData);
            return new DropDownField(this, fieldName,
                JObject.FromObject(fieldOptions.AvalibleOptions.FirstOrDefault(x => x.Value == data.ToString())));
        }

        public override CustomField<JObject> LoadField(JObject data, string fieldName, JObject metaData)
        {
            if (!IsValidForField(data, metaData)) throw new InvalidOperationException($"invalid data for field : {fieldName} => {data}");
            return new DropDownField(this,fieldName,data);
        }

        private static DropDownFieldMetaData GetFieldOptions(JObject fdMetaData)
        {
            if (fdMetaData is null) throw new InvalidOperationException("fdMetaData is Null");

            var fieldOptions = fdMetaData.ToObject<DropDownFieldMetaData>();

            if (fieldOptions.AvalibleOptions is null || fieldOptions.AvalibleOptions.Count is 0)
                throw new InvalidOperationException("drop down options not presesnted");
            return fieldOptions;
        }
    }
}