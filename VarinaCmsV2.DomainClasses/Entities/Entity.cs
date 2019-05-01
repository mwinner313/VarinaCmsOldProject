using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.DomainClasses.Helpers;
using VarinaCmsV2.DomainClasses.Users;

namespace VarinaCmsV2.DomainClasses.Entities
{
    public class Entity : BaseEntity,IEntity<Category>,
        ITemplatedItem,
        IVisibliltyControlled,
        ITaggedItem,
        ICommentibleItem,
        ISoftDeletibleItem,
        ITrackibleItem,
        IAccessRestrictedItem,
        IGlobalizedItem,
        IScheduledItem,
        IHaveRelatedCategoryItem<Category>,
        IUrlable,
        IHaveMetaData

    {
        public string MetaTypeName => nameof(Entity);
        public const string MetaType = nameof(Entity);

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }
        public DateTime PublishDateTime { get; set; }
        public string Url { get; set; }

        /// <summary>
        /// used for programmres to accses this entity in template and template binding
        /// </summary>
        public string Handle { get; set; }

        public int LikeCount { get; set; }
        public bool IsCommentDisabled { get; set; }
        public bool IsVisible { get; set; }
        public int VisitCount { get; set; }
        public AccessType AccessType { get; set; } = AccessType.Public;
        public DateTime CreateDateTime { get; set; }
        public DateTime UpdateDateTime { get; set; }
        public bool IsDeleted { get; set; }
        public string AllowedRolesString { get; set; }

        #region Navigation

        public Category PrimaryCategory { get; set; }
        public Guid PrimaryCategoryId { get; set; }
        public EntityScheme Scheme { get; set; }
        public Guid SchemeId { get; set; }
        public User Creator { get; set; }
        public Guid CreatorId { get; set; }
        public Language Language { get; set; }
        public string LanguageName { get; set; }
        public ICollection<Field> Fields { get; set; }
        public ICollection<Tag> Tags { get; set; }
        public virtual ICollection<Category> RelatedCategories { get; set; }
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

        [NotMapped]
        public ICollection<Comment> Comments { get; set; }

        #endregion
    }
}