using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using VarinaCmsV2.Common;
using VarinaCmsV2.Common.CustomFields;
using VarinaCmsV2.Core.Logic.CustomFields.Fields;
using VarinaCmsV2.Core.Logic.CustomFields.FieldTemplates;

namespace VarinaCmsV2.Core.Logic.CustomFields.Factories
{
    public class EmailFieldFactory : CustomFieldFactory<JObject>
    {
        public EmailFieldFactory() : base("email")
        {
        }
        public override bool IsValidForField(object data, JObject fdMetaData = null)
        {
            return new EmailAddressAttribute().IsValid(data);
        }

        public override bool IsValidForField(JObject data, JObject metaData = null)
        {
            try
            {
                var correctData = data.ToObject<EmailFieldTemplate>();
                if (correctData.EmailAddress != null && new EmailAddressAttribute().IsValid(data)) return true;
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public override CustomField<JObject> CreateNew(string fieldName, object data, JObject fdMetaData)
        {
          return new EmailField(this,fieldName,JObject.FromObject(new EmailFieldTemplate() {EmailAddress = data as string}));
        }

        public override CustomField<JObject> LoadField(JObject data, string fieldName, JObject metaData)
        {
            if (!IsValidForField(data)) throw new InvalidOperationException($"invalid data for field : {fieldName} => {data}");
            return new EmailField(this, fieldName, data);

        }
    }
}
