using System.Web.Mvc;
using Elmah;

namespace VarinaCmsV2.Common.MvcFilters
{
    public class ElmahHandledErrorLoggerFilterAttribute : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.ExceptionHandled)
                ErrorSignal.FromCurrentContext().Raise(context.Exception);
        }
    }
}
