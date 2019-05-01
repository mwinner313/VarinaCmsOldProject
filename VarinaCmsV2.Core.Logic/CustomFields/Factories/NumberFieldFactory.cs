using System;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using VarinaCmsV2.Common;
using VarinaCmsV2.Common.CustomFields;
using VarinaCmsV2.Core.Logic.CustomFields.Fields;
using VarinaCmsV2.Core.Logic.CustomFields.FieldTemplates;

namespace VarinaCmsV2.Core.Logic.CustomFields.Factories
{
    public class NumberFieldFactory:CustomFieldFactory<JObject>
    {
        public NumberFieldFactory() : base("number")
        {
        }

        public override bool IsValidForField(object data, JObject fdMetaData = null)
        {
            float n;
            return (float.TryParse(data.ToString(), out n)) ;
        }
        public override bool IsValidForField(JObject data, JObject metaData = null)
        {
            var numberTemplate = data.ToObject<NumberTemplate>();
            if (numberTemplate?.Number == null) return false;
            if (numberTemplate.Number == "") return true;
            return Regex.IsMatch(numberTemplate.Number, RegexPattern.Number);
        }

        public override CustomField<JObject> CreateNew(string fieldName, object data, JObject fdMetaData)
        {
            return  new NumberField(this,fieldName,JObject.FromObject(new NumberTemplate()
            {
                Number = data.ToString()
            }));
        }

        public override CustomField<JObject> LoadField(JObject data, string fieldName, JObject metaData)
        {
           if(!IsValidForField(data)) throw new InvalidOperationException($"invalid Data for loading Field{fieldName}");
          return new NumberField(this,fieldName, data);
        }

     
    }
}
