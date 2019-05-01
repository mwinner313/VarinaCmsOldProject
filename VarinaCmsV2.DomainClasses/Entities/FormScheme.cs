using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VarinaCmsV2.DomainClasses.Entities
{
    public class FormScheme:BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Handle { get; set; }
        public ICollection<FieldDefenition> FieldDefenitions { get; set; }
        public ICollection<Form> Forms { get; set; }
//        public Portal Portal { get; set; }
//        public Guid PortalId { get; set; }
    }
}
