using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using StructureMap;
using StructureMap.Web;

namespace VarinaCmsV2.IocCofig
{
    public class PrincipalRegistries:Registry
    {
        public PrincipalRegistries()
        {
            For<IPrincipal>().HttpContextScoped().Use(HttpContext.Current?.User ?? Thread.CurrentPrincipal);
        }
    }
}
