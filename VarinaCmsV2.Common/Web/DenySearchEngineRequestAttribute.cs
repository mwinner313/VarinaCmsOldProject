using System.Web.Mvc;

namespace VarinaCmsV2.Common.Web
{
    public class DenySearchEngineRequestAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if(filterContext.HttpContext.Request.Browser.Crawler)
                filterContext.Result= new ContentResult(){Content = "SearchEngines cant access this request"};
            base.OnActionExecuting(filterContext);
        }
    }
}