using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.DomainClasses.Entities.EShop;
using VarinaCmsV2.DomainClasses.Helpers;

namespace VarinaCmsV2.DomainClasses.Entities
{
    public class EntityScheme: BaseEntity, IAccessRestrictedItem,IScheme
    {
        public EntityScheme()
        {
            Entities=new List<Entity>();
        }
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<FieldDefenition> FieldDefenitions { get; set; }
        public ICollection<FieldDefenitionGroup> FieldDefenitionGroups { get; set; }
        public ICollection<Entity> Entities { get; set; }
//        public Portal Portal { get; set; }
//        public Guid PortalId { get; set; }
        public SchemeType Type { get; set; }
        public string Url { get; set; }
        [Column(TypeName = "NVARCHAR")]
        [StringLength(450)]
        [Index("IX_Handle", IsUnique = true)]
        public string Handle { get; set; }
        public string AllowedRolesString { get; set; }
        public AccessType AccessType { get; set; }=AccessType.Public;

        #region notmapped

        [NotMapped]
        public ObservableCollection<AllowedRole> AllowedRoles
        {
            get => this.GetAllowedRoles();
            set => AllowedRolesString = JsonConvert.SerializeObject(value);
        }
     

        #endregion
      
    }
}