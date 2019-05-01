using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StructureMap;
using StructureMap.Web;
using VarinaCmsV2.Core.Contracts.Security;
using VarinaCmsV2.Core.Logic.Security;
using VarinaCmsV2.Core.Logic.Security.UserFilterStrategies;

namespace VarinaCmsV2.IocCofig.ContractRegistries
{
    public class SecutrityRegisteries:Registry
    {
        public SecutrityRegisteries()
        {
            For<ISecurityLogger>().HybridHttpOrThreadLocalScoped().Use<SecurityLogger>();
            For<IRestrictedItemAccessManager>().Use<ItemAccessManager>();
            For<IUserFilterStrategyFactory>().HybridHttpOrThreadLocalScoped().Use<UserFilterStrategyFactory>();
        }
    }
}
