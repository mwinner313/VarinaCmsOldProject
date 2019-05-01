using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using VarinaCmsV2.Core.Contracts.DataServices;

namespace VarinaCmsV2.Web.RouteConstraints
{
    public class LanguageRouteConstraint:IRouteConstraint
    {
        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values,
            RouteDirection routeDirection)
        {
            var lang = values[parameterName].ToString();
            return Startup.Container.GetInstance<ILanguageDataService>().FromCache().Any(x => x.Name == lang);
        }
    }
}