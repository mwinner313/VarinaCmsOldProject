using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using VarinaCmsV2.DomainClasses.Entities.EShop;

namespace VarinaCmsV2.DomainClasses.Entities
{
    public class FieldDefenition : BaseEntity
    {
        public FieldDefenition()
        {
        }
        #region navigation
//        public Portal Portal { get; set; }
//        public Guid PortalId { get; set; }
        public EntityScheme Scheme { get; set; }
        public FormScheme FormScheme { get; set; }
        public Category Category { get; set; }
        public ProductCategory ProductCategory{ get; set; }
        public FieldDefenitionGroup FieldDefenitionGroup { get; set; }
        public Guid? SchemeId { get; set; }
        public Guid? CategoryId { get; set; }
        public Guid? ProductCategoryId { get; set; }
        public Guid? FormSchemeId { get; set; }
        public Guid? FieldDefenitionGroupId { get; set; }
        public ICollection<Field> Fields { get; set; }


        #endregion
        public string Title { get; set; }
        public string Handle { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public bool IsRequired { get; set; }
        public bool ShowInTable { get; set; }
        public bool IsArray { get; set; }
        public int Order { get; set; }
        protected string DefaultValueString { get; set; }
        protected string MetaDataString { get; set; }

        #region Not Mapped

        [NotMapped]
        public JObject MetaData
        {
            get => string.IsNullOrEmpty(MetaDataString) || MetaDataString == "{}" || MetaDataString == "[]" || MetaDataString == "[{}]"
                ? null
                : JObject.Parse(MetaDataString);
            set => MetaDataString = value.ToString();
        }
        [NotMapped]
        public JObject DefaultValue
        {
            get => string.IsNullOrEmpty(DefaultValueString) || DefaultValueString == "{}" || DefaultValueString == "[]" || DefaultValueString == "[{}]"
                ? null
                : JObject.Parse(DefaultValueString);
            set => DefaultValueString = value.ToString();
        }

        #endregion
    }
}