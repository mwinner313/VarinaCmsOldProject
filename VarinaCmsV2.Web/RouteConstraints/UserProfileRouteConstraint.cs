using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Routing;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.Security.Contracts;

namespace VarinaCmsV2.Web.RouteConstraints
{
    public class UserProfileRouteConstraint:IRouteConstraint
    {
        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values,
            RouteDirection routeDirection)
        {
            var appUserManager = Startup.Container.GetInstance<IAppUserManager>();
            return  appUserManager.ExistsByUrl(values[parameterName].ToString()) ;
        }
    }
}