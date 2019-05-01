using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace VarinaCmsV2.DomainClasses.Entities
{
    public class MessageServiceAccount:BaseEntity
    {
        public string Title { get; set; }
        /// <summary>
        /// contact service type e.g email or phone
        /// </summary>
        public string Type { get; set; }
//        public Portal Portal { get; set; }
//        public Guid PortalId { get; set; }
        public bool IsDefaultForUse { get; set; }
        protected string MetaDataString { get; set; }
        [NotMapped]
        public JObject MetaData
        {
            get { return string.IsNullOrEmpty(MetaDataString) ? null : JObject.Parse(MetaDataString); }
            set { MetaDataString = value.ToString(); }
        }

    }
}
