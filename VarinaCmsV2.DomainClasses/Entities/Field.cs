using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json.Linq;

namespace VarinaCmsV2.DomainClasses.Entities
{
    public class Field : BaseEntity
    {
        public FieldDefenition FieldDefenition { get; set; }
        public Guid FieldDefenitionId { get; set; }
        public Entity Entity { get; set; }
        public Form Form { get; set; }
        public Guid? FormId { get; set; }
        public Guid? EntityId { get; set; }
        public Guid? ProductId { get; set; }
//        public Portal Portal { get; set; }
//        public Guid PortalId { get; set; }
        public string RawValueString { get; set; }
        [NotMapped]
        public JObject RawValue
        {
            get => string.IsNullOrEmpty(RawValueString) ? null : JObject.Parse(RawValueString);
            set => RawValueString = value.ToString().Trim();
        }
    }
}