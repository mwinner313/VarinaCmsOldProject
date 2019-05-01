using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.DomainClasses.Contracts;

namespace VarinaCmsV2.DomainClasses.Entities
{
    public class Menu:BaseEntity, IGlobalizedItem
    {
        public string Title { get; set; }
        public ICollection<MenuItem> MenuItems { get; set; } = new List<MenuItem>();
        public string LanguageName { get; set; }
//        public Portal Portal { get; set; }
//        public Guid PortalId { get; set; }
        public Language Language { get; set; }
    }
}
