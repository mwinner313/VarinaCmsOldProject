using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StructureMap.Web;
using VarinaCmsV2.Core.Contracts.ModelFactories;
using VarinaCmsV2.Core.Logic.ModelFactories;

namespace VarinaCmsV2.IocCofig.ContractRegistries
{
    public class ModelFactoryResgistries:StructureMap.Registry
    {
        public ModelFactoryResgistries()
        {
            For<ICheckoutModelFactory>().HybridHttpOrThreadLocalScoped().Use<CheckoutModelFactory>();
            For<ICartModelFactory>().HybridHttpOrThreadLocalScoped().Use<CartModelFactory>();
        }
    }
}
