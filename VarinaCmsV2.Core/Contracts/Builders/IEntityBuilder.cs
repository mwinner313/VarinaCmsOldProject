using System.Collections.Generic;
using System.Threading.Tasks;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.DomainClasses.Entities;

namespace VarinaCmsV2.Core.Contracts.Builders
{
    public interface  IEntityBuilder<T, in TCategory> where T :IEntity<TCategory>
        where TCategory :ICategory
    {
        void SetBuildingEntity(T model, EntityScheme scheme, TCategory primaryCat);
        T GetResult();
    }
}
