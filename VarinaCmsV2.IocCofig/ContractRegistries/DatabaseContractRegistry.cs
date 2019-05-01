using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using StructureMap;
using StructureMap.Web;
using VarinaCmsV2.Core.Contracts.DataBaseContracts;
using VarinaCmsV2.Data.Contracts;
using VarinaCmsV2.Data.DbContexts;

namespace VarinaCmsV2.IocCofig.ContractRegistries
{
    public class DatabaseContractRegistry: Registry
    {
        public DatabaseContractRegistry()
        {
            For<IUnitOfWork>().HybridHttpOrThreadLocalScoped().Use<AppDbContext>();
            For<DbContext>().HybridHttpOrThreadLocalScoped().Use<AppDbContext>();
        }
    }
}
