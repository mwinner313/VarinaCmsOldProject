using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VarinaCmsV2.Common;

namespace VarinaCmsV2.DomainClasses.Entities
{
    public class Language:DbEntity
    {
        public Language()
        {
            
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [MaxLength(5)]
        public string Name { get; set; }
        public string ResourceAddress { get; set; }
//        public Portal Portal { get; set; }
//        public Guid PortalId { get; set; }
    }
}
