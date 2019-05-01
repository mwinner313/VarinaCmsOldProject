using System.Web.Http;
using System.Web.Http.Controllers;
using VarinaCmsV2.Core.Web.Exceptions;
using VarinaCmsV2.Core.Web.Security.Mvc;
using ActionFilterAttribute = System.Web.Http.Filters.ActionFilterAttribute;

namespace VarinaCmsV2.Core.Web.Security.WebApi
{
    public class WebApiCmsAuthorizeActionFilterAttribute:ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (!hasDefinedAllowAnonyous(actionContext))
            {
                CheckForCmsAuthorizeAttr(actionContext);
            }
            base.OnActionExecuting(actionContext);
        }

        private void CheckForCmsAuthorizeAttr(HttpActionContext filterContext)
        {
            if (filterContext.ActionDescriptor.GetCustomAttributes<WebApiCmsAuthorize>().Count == 0
                && filterContext.ControllerContext.ControllerDescriptor.GetCustomAttributes<WebApiCmsAuthorize>().Count==0)
                throw 
                    new NotDefinedCmsAuthorizeAttributeException(filterContext.ActionDescriptor.ActionName,
                                                                 filterContext.ActionDescriptor.ControllerDescriptor.ControllerName);
        }

        private bool hasDefinedAllowAnonyous(HttpActionContext filterContext)
        {
            return filterContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Count != 0
                || filterContext.ActionDescriptor.ControllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Count != 0;
        }
    }
}
