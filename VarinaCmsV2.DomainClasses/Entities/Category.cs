using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.DomainClasses.Entities.Enums;
using VarinaCmsV2.DomainClasses.Helpers;

namespace VarinaCmsV2.DomainClasses.Entities
{
    public class Category : BaseEntity,ICategory,
        IScheme,
        ITemplatedItem,
        IHaveMetaData,
        IParentChildUrlChainedItem<Category>,
        IGlobalizedItem,
        IAccessRestrictedItem,
        ISoftDeletibleItem,
        IUrlable
    {
        public const string MetaTypeName = nameof(Category);
        public string Title { get; set; }
        public string Description { get; set; }
        public string UrlSegment { get; set; }
        public string Handle { get; set; } 
        public bool IsHidden { get; set; }
        public CategoryType CategoryType { get; set; }
        public Guid? SchemeId { get; set; }
        public int Order { get; set; }
        public Guid? ParentId { get; set; }
        [ForeignKey(nameof(Language))]
        public string LanguageName { get; set; }

        public ICollection<FieldDefenitionGroup> FieldDefenitionGroups { get; set; }
        public string Url { get; set; }
        public string AllowedRolesString { get; set; }
        public AccessType AccessType { get; set; }
        public bool IsDeleted { get; set; }

        #region Navigation

        public virtual ICollection<FieldDefenition> FieldDefenitions { get; set; }

        public Language Language { get; set; }
        public EntityScheme Scheme { get; set; }
        public virtual Category Parent { get; set; }
        public virtual ICollection<Category> Childs { get; set; }
//        public Portal Portal { get; set; }
//        public Guid PortalId { get; set; }

        #endregion

        #region NotMapped
        /// <summary>
        /// Premissions That Users At Least Should Have One Of Them To Access This Entity
        /// </summary>
        [NotMapped]
        public ObservableCollection<AllowedRole> AllowedRoles
        {
            get => this.GetAllowedRoles();
            set => AllowedRolesString = JsonConvert.SerializeObject(value);
        }
        #endregion
    }
}
