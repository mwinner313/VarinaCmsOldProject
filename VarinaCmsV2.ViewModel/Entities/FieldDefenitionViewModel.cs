using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json.Linq;

namespace VarinaCmsV2.ViewModel.Entities
{
    public class FieldDefenitionViewModel : BaseVeiwModel
    {
     
        public string Title { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        [Required]
        public bool IsArray { get; set; }
        public string Handle { get; set; }
        public bool IsRequired { get; set; }
        public bool ShowInTable { get; set; }
        public Guid? FieldDefenitionGroupId { get; set; }
        public int Order { get; set; }
        public Guid? SchemeId { get; set; }
        public Guid? CategoryId { get; set; }
        public Guid? ProductCategoryId { get; set; }
        public Guid? FormSchemeId { get; set; }
        public JObject MetaData { get; set; }
        public JObject DefaultValue { get; set; }

    }
}