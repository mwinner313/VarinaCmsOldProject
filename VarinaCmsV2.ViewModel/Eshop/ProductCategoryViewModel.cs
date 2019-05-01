using System;
using System.Collections.Generic;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.DomainClasses.Entities.Enums;
using VarinaCmsV2.ViewModel.Entities;
using VarinaCmsV2.ViewModel.Eshop.Discount;
using VarinaCmsV2.ViewModel.Meta;

namespace VarinaCmsV2.ViewModel.Eshop
{
    public class ProductCategoryViewModel:BaseVeiwModel,IUrlableViewModel
    {
        public List<FieldDefenitionViewModel> FieldDefenitions { get; set; }
        public List<FieldDefenitionGroupViewModel> FieldDefenitionGroups { get; set; }
        public List<ProductCategoryViewModel> Childs { get; set; }
        public List<MetaDataViewModel> MetaData { get; set; } = new List<MetaDataViewModel>();
        public bool IsHidden { get; set; }
        public string UrlSegment { get; set; }
        public string Url { get; set; }
        public CategoryType CategoryType { get; set; }
        public Guid? ParentId { get; set; }
        public int Order { get; set; }
        public string ParentTitle { get; set; }
        public string Description { get; set; }
        public string Handle { get; set; }
        public string DisplayName { get; set; }
        public string Title { get; set; }
        public AccessType AccessType { get; set; }
        public EntitySchemeViewModel Scheme { get; set; }
        public string ToUrl { get; set; } = "";
        public string ToFullUrl { get; set; } = "";
    }
}