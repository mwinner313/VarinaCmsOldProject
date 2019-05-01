using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using VarinaCmsV2.Common.WebApi;
using VarinaCmsV2.Core.Web.Security.WebApi;

namespace VarinaCmsV2.IocCofig
{
    public class WebApiCommonRegistries:StructureMap.Registry
    {
        public WebApiCommonRegistries()
        {
            For<IHttpActionInvoker>().Singleton().Use<ApiInjectingActionInvoker>();
            For<ApiControllerActionInvoker>().Singleton().Use<ApiInjectingActionInvoker>();
        }
    }
}
