using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using VarinaCmsV2.Common.CustomFields;
using VarinaCmsV2.Core.Logic.CustomFields.FieldTemplates;

namespace VarinaCmsV2.Core.Logic.CustomFields.Fields
{
    public class FileField:CustomField<JObject>
    {
        public FileField(CustomFieldFactory<JObject> factory, string fieldName, JObject value) : base(factory, fieldName, value)
        {
        }

        public FileField(CustomFieldFactory<JObject> factory, string fieldName) : base(factory, fieldName)
        {
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public override object ToLiquid()
        {
            return Value.ToObject<FileFieldTemplate>();
        }

        public override string Veiw()
        {
            return Value.ToObject<FileFieldTemplate>()?.Path;
        }
    }
}
