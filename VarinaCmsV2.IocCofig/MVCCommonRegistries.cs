using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using System.Web.Mvc.Async;
using StructureMap;
using VarinaCmsV2.Common.WebApi;
using IFilterProvider = System.Web.Http.Filters.IFilterProvider;

namespace VarinaCmsV2.IocCofig
{
    public class MvcCommonRegistries:Registry
    {
        public MvcCommonRegistries()
        {
        For<IAsyncActionInvoker>().Use<Common.MVC.InjectingActionInvoker>();
        For<IActionInvoker>().Use<Common.MVC.InjectingActionInvoker>();
        }
    }
}
