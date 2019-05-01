using System;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json.Linq;
using VarinaCmsV2.DomainClasses.Contracts;

namespace VarinaCmsV2.DomainClasses.Entities.Widgets
{
    public class Widget : BaseEntity, ITemplatedItem,IGlobalizedItem
    {
        public int  Order { get; set; }
        public string Title { get; set; }
        public WidgetContainer Container { get; set; }
        public Language Language { get; set; }
        public string LanguageName { get; set; }
        public Guid ContainerId { get; set; }
        public string Type { get; set; }
        public string Handle { get; set; }
        public string MetaDataString { get; set; }
//        public Portal Portal { get; set; }
//        public Guid PortalId { get; set; }
        [NotMapped]
        public JObject MetaData
        {
            get { return string.IsNullOrEmpty(MetaDataString) ? null : JObject.Parse(MetaDataString); }
            set { MetaDataString = value.ToString(); }
        }


    }
}
