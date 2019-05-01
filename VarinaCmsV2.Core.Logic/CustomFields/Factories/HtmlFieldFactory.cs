using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotLiquid.Tags;
using Newtonsoft.Json.Linq;
using VarinaCmsV2.Common.CustomFields;
using VarinaCmsV2.Core.Logic.CustomFields.Fields;
using VarinaCmsV2.Core.Logic.CustomFields.FieldTemplates;

namespace VarinaCmsV2.Core.Logic.CustomFields.Factories
{
    public class HtmlFieldFactory : CustomFieldFactory<JObject>
    {
        public HtmlFieldFactory() : base("html")
        {
        }
        //todo sanitize and img responsive class
        public override bool IsValidForField(object data, JObject fdMetaData = null)
        {
            return true;
        }

        public override bool IsValidForField(JObject data, JObject metaData = null)
        {
            try
            {
                var correctData = data.ToObject<HtmlFieldTemplate>();
                if (correctData.Content != null) return true;
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public override CustomField<JObject> CreateNew(string fieldName, object data, JObject fdMetaData)
        {
            throw new NotImplementedException();
        }

        public override CustomField<JObject> LoadField(JObject data, string fieldName, JObject metaData)
        {
            if (!IsValidForField(data)) throw new InvalidOperationException($"invalid data for field : {fieldName} => {data}");
            return new HtmlField(this, fieldName, data);
        }
    }
}
