using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using StructureMap.Attributes;
using VarinaCmsV2.Security.Contracts;
using AuthorizationContext = System.Web.Mvc.AuthorizationContext;

namespace VarinaCmsV2.Core.Web.Security.Mvc
{
    public class MvcCmsAuthorizeAttribute : AuthorizeAttribute
    {
        public string Premissions { get; set; }
        [SetterProperty]
        public IIdentityManager IdentityManager { get; set; }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (string.IsNullOrEmpty(Premissions))
                return base.AuthorizeCore(httpContext);
            return base.AuthorizeCore(httpContext) && IdentityManager.HasPremission(httpContext.User, Premissions);
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
                filterContext.Result = new HttpUnauthorizedResult();
            base.HandleUnauthorizedRequest(filterContext);
        }
    }
}
