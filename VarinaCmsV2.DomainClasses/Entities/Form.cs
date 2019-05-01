using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.DomainClasses.Users;

namespace VarinaCmsV2.DomainClasses.Entities
{
    public class Form : BaseEntity, IOptionalTrackibleItem, ISoftDeletibleItem
    {
        public ICollection<Field> Fields { get; set; }
//        public Portal Portal { get; set; }
//        public Guid PortalId { get; set; }
        public FormScheme FormScheme { get; set; }
        public Guid FormSchemeId { get; set; }
        public DateTime UpdateDateTime { get; set; }
        public DateTime CreateDateTime { get; set; }
        public User Creator { get; set; }
        public Guid? CreatorId { get; set; }
        public bool IsSeen{ get; set; }
        public bool IsDeleted { get; set; }
    }
}
