using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Build.Framework;
using VarinaCmsV2.Common.ValidationAttributes;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.DomainClasses.Entities;
using VarinaCmsV2.DomainClasses.Entities.Enums;
using VarinaCmsV2.ViewModel.Entities;
using VarinaCmsV2.ViewModel.Meta;
using VarinaCmsV2.ViewModel.User;

namespace VarinaCmsV2.ViewModel.Category
{
    public class CategoryAddOrUpdateViewModel : BaseVeiwModel
    {
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }

        public string UrlSegment { get; set; }

        [NoSpace(ErrorMessage = "handle should not have space character")]
        public string Handle { get; set; }

        public int Order { get; set; }
        public bool IsHidden { get; set; }
        public CategoryType CategoryType { get; set; }
        public Guid? SchemeId { get; set; }
        public Guid? ParentId { get; set; }
        public AccessType AccessType { get; set; }
        public string LanguageName { get; set; }
        public List<RoleViewModel> AllowedRoles { get; set; }
        public List<FieldDefenitionAddOrUpdateModel> FieldDefenitions { get; set; }
        public List<MetaDataAddOrUpdateViewModel> MetaData { get; set; } = new List<MetaDataAddOrUpdateViewModel>();
        public List<FieldDefenitionGroupAddOrUpdateModel> FieldDefenitionGroups { get; set; }
    }
}