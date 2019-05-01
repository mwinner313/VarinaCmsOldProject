using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using VarinaCmsV2.Common.ValidationAttributes;

namespace VarinaCmsV2.ViewModel.Entities
{
    public class EntityAddOrUpdateViewModel:BaseVeiwModel, IEntityViewModel
    {
        public List<FieldAddOrUpdateViewModel> Fields { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [NoSpace(ErrorMessage = "handle should not have space character")]
        public string Handle { get; set; }
        public bool IsVisible { get; set; }
        public List<TagViewModel> Tags { get; set; }
        public string Url { get; set; }
        [Required]
        public string PublishDateTime { get; set; }
        [Required]
        public Guid SchemeId { get; set; }
        [Required]
        public Guid PrimaryCategoryId { get; set; }
        [Required]
        public string LanguageName { get; set; }
        public List<RolePremissionViewModel> AllowedRoles { get; set; }
        public List<RelatedCategoryViewModel> RelatedCategories { get; set; }
    }
}
