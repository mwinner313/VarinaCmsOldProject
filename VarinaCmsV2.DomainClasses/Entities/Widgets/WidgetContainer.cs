using System;
using System.Collections.Generic;

namespace VarinaCmsV2.DomainClasses.Entities.Widgets
{
    public class WidgetContainer:BaseEntity
    {
        public string Handle { get; set; }
        public string Title { get; set; }
        public ICollection<Widget> Widgets { get; set; }
//        public Portal Portal { get; set; }
//        public Guid PortalId { get; set; }
    }
}