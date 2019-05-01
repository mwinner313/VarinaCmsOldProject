using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.DomainClasses.Entities;

namespace VarinaCmsV2.DomainClasses.Contracts
{
    public interface IEntity<TCategory> where TCategory :ICategory
    {
        string Url { get; set; }
        EntityScheme Scheme { get; set; }
        TCategory PrimaryCategory { get; set; }
        ICollection<Field> Fields { get; set; }
        string Title { get; set; }
        string Handle { get; set; }
    }
}
