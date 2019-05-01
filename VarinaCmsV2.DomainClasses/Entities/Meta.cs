using System;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json.Linq;

namespace VarinaCmsV2.DomainClasses.Entities
{
    public class Meta : BaseEntity
    {
        public Guid TargetId { get; set; }
        public string TargetType { get; set; }
        public string MetaName { get; set; }
        protected string MetaDataString { get; set; }
        [NotMapped]
        public JObject MetaData
        {
            get => string.IsNullOrEmpty(MetaDataString) ? null : JObject.Parse(MetaDataString);
            set => MetaDataString = value.ToString();
        }
        //public Portal Portal { get; set; }
        //public Guid PortalId { get; set; }
    }
}
