using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using VarinaCmsV2.Core.Contracts.DataServices;

namespace VarinaCmsV2.Web.RouteConstraints
{
    public class EntitySchemeUrlConstraint:IRouteConstraint
    {
        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values,
            RouteDirection routeDirection)
        {
            var schemeUrl = values[parameterName].ToString();
            return !string.IsNullOrEmpty(schemeUrl) &&
                Startup.Container.GetInstance<IEntitySchemeDataService>().Query
                       .Any(x => x.Url == schemeUrl);
        }
    }
}