using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Filters;
using Elmah;

namespace VarinaCmsV2.Common.WebApiFilters
{
    public class ElmahHandledErrorLoggerFilterAttribute : System.Web.Http.Filters.ExceptionFilterAttribute
    {
        public override void OnException(
        System.Web.Http.Filters.HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Exception != null)
               ErrorSignal.FromCurrentContext().Raise(actionExecutedContext.Exception);

            base.OnException(actionExecutedContext);
        }
    }
}
