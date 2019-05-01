using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace VarinaCmsV2.DomainClasses.Entities
{
    public class Setting:BaseEntity
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Icon { get; set; }
        public string TagsForSearch { get; set; }
        protected string RawValue { get; set; }
        public string RolesToAccess { get; set; }
        //public Portal Portal { get; set; }
        //public Guid PortalId { get; set; }
        [NotMapped]
        public JObject Data
        {
            get => string.IsNullOrEmpty(RawValue) ? null : JObject.Parse(RawValue);
            set => RawValue = value.ToString();
        }
    }
}
