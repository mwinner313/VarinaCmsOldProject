using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VarinaCmsV2.Common;
using VarinaCmsV2.DomainClasses.Contracts;

namespace VarinaCmsV2.DomainClasses.Entities
{
    public class BaseEntity: DbEntity, IBaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
