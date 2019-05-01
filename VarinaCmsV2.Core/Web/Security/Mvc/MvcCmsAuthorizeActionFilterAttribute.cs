using System.Web.Mvc;
using VarinaCmsV2.Core.Web.Exceptions;
using ActionFilterAttribute = System.Web.Mvc.ActionFilterAttribute;

namespace VarinaCmsV2.Core.Web.Security.Mvc
{
    public class MvcCmsAuthorizeActionFilterAttribute:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!hasDefinedAllowAnonyous(filterContext))
            {
                CheckForCmsAuthorizeAttr(filterContext);
            }
            base.OnActionExecuting(filterContext);
        }

        private void CheckForCmsAuthorizeAttr(ActionExecutingContext filterContext)
        {
           if (filterContext.ActionDescriptor.GetCustomAttributes(typeof(MvcCmsAuthorizeAttribute),false).Length==0)
                throw 
                    new NotDefinedCmsAuthorizeAttributeException(filterContext.ActionDescriptor.ActionName,
                                                                 filterContext.ActionDescriptor.ControllerDescriptor.ControllerName);
        }

        private bool hasDefinedAllowAnonyous(ActionExecutingContext filterContext)
        {
            return 
                filterContext.ActionDescriptor.GetCustomAttributes(typeof(AllowAnonymousAttribute),false) .Length!=0
               || filterContext.ActionDescriptor.ControllerDescriptor.GetCustomAttributes(typeof(AllowAnonymousAttribute),false) .Length!=0;
        }
    }
}
