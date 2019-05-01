using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StructureMap.Web;
using VarinaCmsV2.Core.Contracts;
using VarinaCmsV2.Core.Logic;

namespace VarinaCmsV2.IocCofig.ContractRegistries
{
    public class WorkContextRegistery:StructureMap.Registry

    {
        public WorkContextRegistery()
        {
            For<IWorkContext>().HybridHttpOrThreadLocalScoped().Use<WebWorkContext>();
        }
    }
}
