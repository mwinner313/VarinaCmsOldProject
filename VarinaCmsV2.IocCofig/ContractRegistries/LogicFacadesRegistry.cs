using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StructureMap.Web;
using VarinaCmsV2.Core.Logic.Facades;
using VarinaCmsV2.Core.LogicFacade;

namespace VarinaCmsV2.IocCofig.ContractRegistries
{
    public class LogicFacadesRegistry:StructureMap.Registry
    {
        public LogicFacadesRegistry()
        {
            For<IEntityFacade>().HybridHttpOrThreadLocalScoped().Use<EntityFacade>();
            For<IProductFacade>().HybridHttpOrThreadLocalScoped().Use<ProductFacade>();
            For<IEntitySchemeFacade>().HybridHttpOrThreadLocalScoped().Use<EntitySchemeFacade>();
            For<IFieldDefentionFacade>().HybridHttpOrThreadLocalScoped().Use<FieldDefenitionFacade>();
            For<IDiscountFacade>().HybridHttpOrThreadLocalScoped().Use<DiscountFacade>();
        }
    }
}
