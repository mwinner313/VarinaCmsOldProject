using Newtonsoft.Json.Linq;

namespace VarinaCmsV2.Common.CustomFields
{

    public abstract class CustomFieldFactory<TSavingData> where TSavingData : class
    {
        protected CustomFieldFactory(string fieldType)
        {
            FieldType = fieldType;
        }

        protected CustomFieldFactory()
        {

        }

        public readonly string FieldType;
        public abstract bool IsValidForField(object data, JObject fdMetaData);
        public abstract bool IsValidForField(TSavingData data, JObject fdMetaData);

        public abstract CustomField<TSavingData> CreateNew(string fieldName, object data, JObject fdMetaData);
        public abstract CustomField<TSavingData> LoadField(TSavingData data, string fieldName, JObject metaData);
    }
}
