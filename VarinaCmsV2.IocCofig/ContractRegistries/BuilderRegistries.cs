using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StructureMap.Web;
using VarinaCmsV2.Core.Contracts.Builders;
using VarinaCmsV2.Core.Logic.Builders;
using VarinaCmsV2.DomainClasses.Entities;
using VarinaCmsV2.DomainClasses.Entities.EShop;

namespace VarinaCmsV2.IocCofig.ContractRegistries
{
    public class BuilderRegistries:StructureMap.Registry
    {
        public BuilderRegistries()
        {
            For<IEntityBuilder<Entity,Category>>().HybridHttpOrThreadLocalScoped().Use<GenericIEntityBuilder<Entity,Category>>();
            For<IEntityBuilder<Product, ProductCategory>>().HybridHttpOrThreadLocalScoped().Use<GenericIEntityBuilder<Product, ProductCategory>>();
            For<ISchemeBuilder<EntityScheme>>().HybridHttpOrThreadLocalScoped().Use<SchemeBuilder<EntityScheme>>();
        }
    }
}
