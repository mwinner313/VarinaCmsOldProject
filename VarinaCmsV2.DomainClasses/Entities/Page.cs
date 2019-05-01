using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.DomainClasses.Helpers;
using VarinaCmsV2.DomainClasses.Users;

namespace VarinaCmsV2.DomainClasses.Entities
{
    public class Page : BaseEntity,
        ITemplatedItem,
        ICommentibleItem,
        ISoftDeletibleItem,
        ITrackibleItem,
        IParentChildUrlChainedItem<Page>,
        IScheduledItem,
        ITaggedItem,
        IAccessRestrictedItem,
        IGlobalizedItem,
        IUrlable

    {
        public Page()
        {
            Tags = new List<Tag>();
        }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCommentDisabled { get; set; }
        //TODO sanitize checking correction 
        public string HtmlContent { get; set; }
        /// <summary>
        /// used for url and template binding
        /// </summary>
        [Column(TypeName = "NVARCHAR")]
        [StringLength(450)]
        [Index("IX_Handle", IsUnique = true)]
        public string Handle { get; set; }
        public string Url { get; set; }
        public string UrlSegment { get; set; }
        public bool IsDeleted { get; set; }
        public string AllowedRolesString { get; set; }
        public AccessType AccessType { get; set; } = AccessType.Public;
        public DateTime PublishDateTime { get; set; } = DateTime.Now;
        public DateTime UpdateDateTime { get; set; } = DateTime.Now;
        public DateTime CreateDateTime { get; set; } = DateTime.Now;
        #region Navigation
        //public Portal Portal { get; set; }
        //public Guid PortalId { get; set; }
        public virtual ICollection<Page> Childs { get; set; } 
        public virtual Page Parent { get; set; }
        public Guid? ParentId { get; set; }
        public string LanguageName { get; set; }
        public Language Language { get; set; }
        public Guid CreatorId { get; set; }
        public virtual User Creator { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
        #endregion
        #region NotMapped
        //TODO test
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
