using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VarinaCmsV2.DomainClasses.Entities
{
    public class UserAttribute:BaseEntity
    {
        public string Value { get; set; }
        public string Name { get; set; }
        public Guid UserId { get; set; }
        //public Portal Portal { get; set; }
        //public Guid PortalId { get; set; }
    }
}
