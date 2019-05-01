using System;
using System.IO;
using Newtonsoft.Json.Linq;
using VarinaCmsV2.Common.CustomFields;
using VarinaCmsV2.Core.Logic.CustomFields.FieldTemplates;
using VarinaCmsV2.Core.Logic.CustomFields.Fields;

namespace VarinaCmsV2.Core.Logic.CustomFields.Factories
{
    public class ImageFieldFactory : CustomFieldFactory<JObject>
    {
        public ImageFieldFactory() : base("image")
        {
        }

        /// <summary>
        ///  
        /// </summary>
        /// <param name="data">should be a string to a specified path</param>
        /// <param name="fdMetaData"></param>
        /// <returns></returns>
        public override bool IsValidForField(object data, JObject fdMetaData = null)
        {
           return File.Exists(data.ToString());
        }

        public override bool IsValidForField(JObject data, JObject metaData = null)
        {
            try
            {
                var correctData = data.ToObject<ImageTemplate>();
                if (correctData.Path != null) return true;
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
            if (!IsValidForField(data)) throw new InvalidOperationException($"invalid Data for loading Field{fieldName}");
            return new ImageField(this, fieldName, data);
        }
    }
}