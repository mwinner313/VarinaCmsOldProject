using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.DomainClasses.Entities;

namespace VarinaCmsV2.DomainClasses.Contracts
{
    public interface ICategory
    {
        ICollection<FieldDefenition> FieldDefenitions { get; set; }
        ICollection<FieldDefenitionGroup> FieldDefenitionGroups { get; set; }

    }
}
