using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.ViewModel.Category;


namespace VarinaCmsV2.ViewModel.Entities
{
    public class EntityViewModel:BaseVeiwModel,IUrlableViewModel
    {
        public List<FieldViewModel> Fields { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsVisible { get; set; }
        public string Handle { get; set; }

        [Url]
        public string Url { get; set; }
        public string CreateDateTime { get; set; }
        public string UpdateDateTime { get; set; }
        public string PublishDateTime { get; set; }
        public Guid SchemeId { get; set; }
        public string SchemeTitle { get; set; }
        public Guid PrimaryCategoryId { get; set; }
        public CategoryViewModel PrimaryCategory { get; set; }
        public string PrimaryCategoryTitle { get; set; }
        public string LanguageName { get; set; }
        public string CreatorUserName { get; set; }
        public List<TagViewModel> Tags { get; set; }
        public List<RolePremissionViewModel> AllowedRoles { get; set; }
        public List<CategoryViewModel> RelatedCategories { get; set; }
        public string ToUrl { get; set; }
        public string ToFullUrl { get; set; }
    }
}
