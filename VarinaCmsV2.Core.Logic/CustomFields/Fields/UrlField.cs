using Newtonsoft.Json.Linq;
using VarinaCmsV2.Common.CustomFields;
using VarinaCmsV2.Core.Logic.CustomFields.Factories;
using VarinaCmsV2.Core.Logic.CustomFields.FieldTemplates;

namespace VarinaCmsV2.Core.Logic.CustomFields.Fields
{
    internal class UrlField : CustomField<JObject>
    {
      

        public UrlField(UrlFieldFactory urlFieldFactory, string fieldName, JObject data) : base(urlFieldFactory,fieldName,data)
        {
        }

        public override string ToString()
        {
            return base.Value.ToString();
        }

        public override object ToLiquid()
        {
            return Value.ToObject<UrlFieldTemplate>().Url;
        }

        public override string Veiw()
        {
            return Value.ToObject<UrlFieldTemplate>()?.Url;
        }
    }
}