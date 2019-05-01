using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using VarinaCmsV2.DomainClasses.Contracts;

namespace VarinaCmsV2.DomainClasses.Entities
{
    public class Tag: BaseEntity, 
        ITemplatedItem,
        IUrlable,
        IGlobalizedItem

    {
        public string Title { get; set; }
        public string Url { get; set; }
        /// <summary>
        /// used for programmres to accses this entity in template and template binding
        /// </summary>
        [Column(TypeName = "NVARCHAR")]
        [StringLength(450)]
        [Index("IX_Handle", IsUnique = true)]
        public string Handle { get; set; }
        public EntityScheme Scheme { get; set; }
        public Guid? SchemeId { get; set; }
        public ICollection<Page> Pages { get; set; }
        [Required]
        public string LanguageName { get; set; }
        public Language Language { get; set; }
        //public Portal Portal { get; set; }
        //public Guid PortalId { get; set; }
    }
}
