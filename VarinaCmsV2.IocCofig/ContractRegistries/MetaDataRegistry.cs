using StructureMap.Web;
using System.Collections.Generic;
using VarinaCmsV2.Core.Contracts.MetaData;
using VarinaCmsV2.Core.Logic.MetaData;
using VarinaCmsV2.DomainClasses.Contracts;

namespace VarinaCmsV2.IocCofig.ContractRegistries
{
    public class MetaDataRegistry:StructureMap.Registry
    {
        public MetaDataRegistry()
        {
            For<IMenuItemMetaDataHandler>().HybridHttpOrThreadLocalScoped().Use<MenuItemMetaDataHandler>();
            For<List<IMetaDataLiquidAdapter>>().Singleton().Use("registring metaDataAdapters", c => TypeHelper.FindListOfByContainer<IMetaDataLiquidAdapter>(c));
            For<IMetaDataLiquidAdapterFactory>().Singleton().Use<SimpleMetaDataLiquidAdapterFactory>();
        }
    }
}
