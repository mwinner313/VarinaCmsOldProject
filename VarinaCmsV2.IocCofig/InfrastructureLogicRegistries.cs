using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.Core.Infrastructure;

namespace VarinaCmsV2.IocCofig
{
    public class InfrastructureLogicRegistries : StructureMap.Registry
    {
        public InfrastructureLogicRegistries()
        {
            For<List<IBaseBeforeAddingEntityLogic>>().Singleton().Use("BaseBeforeAddingEntityLogic", TypeHelper.FindListOf<IBaseBeforeAddingEntityLogic>);

            For<List<BaseAfterAddingEntityLogic>>().Singleton().Use("BaseAfterAddingEntityLogic", TypeHelper.FindListOf<BaseAfterAddingEntityLogic>);

            For<List<IBaseBeforeUpdatingEntityLogic>>().Singleton().Use("BaseBeforeUpdatingEntityLogic", TypeHelper.FindListOf<IBaseBeforeUpdatingEntityLogic>);

            For<List<IBaseAfterUpdatingEntityLogic>>().Singleton().Use("BaseAfterUpdatingEntityLogic", TypeHelper.FindListOf<IBaseAfterUpdatingEntityLogic>);

            For<List<IBaseBeforeDeleteEntityLogic>>().Singleton().Use("BaseBeforeDeleteEntityLogic", TypeHelper.FindListOf<IBaseBeforeDeleteEntityLogic>);

            For<List<BaseAfterDeleteEntityLogic>>().Singleton().Use("BaseAfterDeleteEntityLogic", TypeHelper.FindListOf<BaseAfterDeleteEntityLogic>);
        }
    }
}
