using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json.Linq;
using StructureMap.Attributes;
using VarinaCmsV2.Common.CustomFields;
using VarinaCmsV2.Core.Contracts;
using VarinaCmsV2.Core.Logic.CustomFields.Fields;
using VarinaCmsV2.Core.Logic.CustomFields.FieldTemplates;
using VarinaCmsV2.Core.Logic.Helpers;
using VarinaCmsV2.FileManager;

namespace VarinaCmsV2.Core.Logic.CustomFields.Factories
{
    public class FileFieldFactory : CustomFieldFactory<JObject>
    {
       
        private readonly List<string> _allowedExts = new List<string>()
      {
          ".pdf",
          ".docx",
          ".zip",
          ".csv",
          ".mp4",
          ".rar",
          ".psd",
          ".jpg",
          ".jpeg",
          ".png",
      };
        public FileFieldFactory() : base("file")
        {

        }
        public override bool IsValidForField(object data, JObject fdMetaData = null)
        {
            if (data is FileFieldDataWrapper file && _allowedExts.Contains(new FileInfo(file.Path).Extension)) return true;
            return false;
        }

        public override bool IsValidForField(JObject data, JObject metaData = null)
        {
          var file=  data.ToObject<FileFieldTemplate>();

            return file.Path!=null&& _allowedExts.Contains(new FileInfo(file.Path).Extension);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="data"></param>
        /// <param name="fdMetaData"></param>
        /// <returns></returns>
        public override CustomField<JObject> CreateNew(string fieldName, object data, JObject fdMetaData)
        {
            var file = data as FileFieldDataWrapper;
            return new FileField(this, fieldName, JObject.FromObject(new FileFieldTemplate()
            {
                Name = file.Name,
                Path = file.Path,
                Extention = Path.GetExtension(file.Path)
            }));

        }

        public override CustomField<JObject> LoadField(JObject data, string fieldName, JObject metaData)
        {
           throw new NotImplementedException();
        }
    }
}
