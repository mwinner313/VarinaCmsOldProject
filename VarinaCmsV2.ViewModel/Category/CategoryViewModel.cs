using System;
using System.Collections.Generic;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.DomainClasses.Entities.Enums;
using VarinaCmsV2.ViewModel.Entities;
using VarinaCmsV2.ViewModel.Meta;
using VarinaCmsV2.ViewModel.User;

namespace VarinaCmsV2.ViewModel.Category
{
    public class CategoryViewModel : BaseVeiwModel, IUrlableViewModel
    {
        public string Description { get; set; }
        public string Title { get; set; }
        public string DisplayName { get; set; }
        public string UrlSegment { get; set; }
        public string Handle { get; set; }
        public bool IsHidden { get; set; }
        public CategoryType CategoryType { get; set; }
        public Guid? SchemeId { get; set; }
        public Guid? ParentId { get; set; }
        public AccessType AccessType { get; set; }
        public int Order { get; set; }
        public string SchemeTitle { get; set; }
        public string SchemeHandle { get; set; }
        public string ParentTitle { get; set; }
        public string LanguageName { get; set; }
        public string Url { get; set; }
        public List<CategoryViewModel> Childs { get; set; }
        public List<FieldDefenitionViewModel> FieldDefenitions { get; set; }
        public List<FieldDefenitionGroupViewModel> FieldDefenitionGroups { get; set; }
        public List<RoleViewModel> AllowedRoles { get; set; }
        public List<MetaDataViewModel> MetaData { get; set; } = new List<MetaDataViewModel>();
        public string ToUrl { get; set; }
        public string ToFullUrl { get; set; }
    }
}
