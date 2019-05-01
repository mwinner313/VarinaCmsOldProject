using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace VarinaCmsV2.ViewModel.Entities
{
    public class FieldDefenitionAddOrUpdateModel:BaseVeiwModel
    {
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public string Handle { get; set; }
        public bool IsRequired { get; set; }
        public bool ShowInTable { get; set; }

        public bool IsArray { get; set; }
        [Required]
        public int Order { get; set; }
        public string DefaultValueString { get; set; }
        public string MetaDataString { get; set; }
        public Guid? SchemeId { get; set; }
        public Guid? ProductCategoryId { get; set; }
        public Guid? FieldDefenitionGroupId { get; set; }
        public Guid? CategoryId { get; set; }
        public Guid? FormSchemId { get; set; }
        public JObject MetaData
        {
            get => string.IsNullOrEmpty(MetaDataString)
                ? null
                : JObject.Parse(MetaDataString);
            set => MetaDataString = value?.ToString();
        }
        public JObject DefaultValue
        {
            get => string.IsNullOrEmpty(DefaultValueString)
                ? null
                : JObject.Parse(DefaultValueString);
            set => DefaultValueString = value?.ToString();
        }

       
    }
}
