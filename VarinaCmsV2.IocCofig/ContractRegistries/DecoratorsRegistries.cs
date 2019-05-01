using StructureMap.Web;
using System.Collections.Generic;
using VarinaCmsV2.Core.Contracts.Decorators;
using VarinaCmsV2.ViewModel.Menu;

namespace VarinaCmsV2.IocCofig.ContractRegistries
{
    public class DecoratorsRegistries:StructureMap.Registry
    {
        public DecoratorsRegistries()
        {
            For<List<LiquidDataDecorator>>().HybridHttpOrThreadLocalScoped().Use("LiquidDataDecorators", TypeHelper.FindListOf<LiquidDataDecorator>);
            For<List<MenuItemLiquidDecorator>>().HybridHttpOrThreadLocalScoped().Use("registring menuitem data decorators", TypeHelper.FindListOfByContainer<MenuItemLiquidDecorator>);
        }
    }

}
