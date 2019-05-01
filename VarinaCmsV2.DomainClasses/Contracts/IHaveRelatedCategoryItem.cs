using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.DomainClasses.Entities;

namespace VarinaCmsV2.DomainClasses.Contracts
{
    public interface IHaveRelatedCategoryItem<TCategory> where TCategory :ICategory
    {
         ICollection<TCategory> RelatedCategories { get; set; }
    }
}
