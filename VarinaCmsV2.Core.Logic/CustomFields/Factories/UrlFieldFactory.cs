using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using VarinaCmsV2.Common.CustomFields;
using VarinaCmsV2.Core.Logic.CustomFields.Fields;
using VarinaCmsV2.Core.Logic.CustomFields.FieldTemplates;

namespace VarinaCmsV2.Core.Logic.CustomFields.Factories
{
    public class UrlFieldFactory:CustomFieldFactory<JObject>
    {
        public UrlFieldFactory():base("url")
        {
            
        }
        public override bool IsValidForField(object data, JObject fdMetaData = null)
        {
            return new UrlAttribute().IsValid(data);
        }

        public override bool IsValidForField(JObject data, JObject metaData = null)
        {
            var correctData = data.ToObject<UrlFieldTemplate>();
            return correctData.Url != null && new UrlAttribute().IsValid(correctData.Url);
        }

        public override CustomField<JObject> CreateNew(string fieldName, object data, JObject fdMetaData)
        {
           return new UrlField(this,fieldName,JObject.FromObject(new UrlFieldTemplate() {Url = data as string}));
        }

        public override CustomField<JObject> LoadField(JObject data, string fieldName, JObject metaData)
        {
            if (!IsValidForField(data)) throw new InvalidOperationException($"invalid Data for loading Field{fieldName}");
            return new UrlField(this, fieldName, data);
        }
    }
}
