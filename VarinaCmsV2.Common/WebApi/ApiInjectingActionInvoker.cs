using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using StructureMap;
using WebGrease.Css.Extensions;

namespace VarinaCmsV2.Common.WebApi
{
    public class ApiInjectingActionInvoker : ApiControllerActionInvoker
    {
        private readonly IContainer _container;

        public ApiInjectingActionInvoker(IContainer container)
        {
            _container = container;
        }

        public override Task<HttpResponseMessage> InvokeActionAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            actionContext.ActionDescriptor.GetFilters().ForEach(_container.BuildUp);
            return base.InvokeActionAsync(actionContext, cancellationToken);
        }
       
    }
}
