using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Async;
using StructureMap;
using WebGrease.Css.Extensions;

namespace VarinaCmsV2.Common.MVC
{
    public class InjectingActionInvoker : AsyncControllerActionInvoker
    {
        private readonly IContainer _container;

        public InjectingActionInvoker(IContainer container)
        {
            _container = container;
        }

        protected override FilterInfo GetFilters(
            ControllerContext controllerContext,
            ActionDescriptor actionDescriptor)
        {
            var info = base.GetFilters(controllerContext, actionDescriptor);

            info.AuthorizationFilters.ForEach(_container.BuildUp);
            info.ActionFilters.ForEach(_container.BuildUp);
            info.ResultFilters.ForEach(_container.BuildUp);
            info.ExceptionFilters.ForEach(_container.BuildUp);

            return info;
        }
    }
}
