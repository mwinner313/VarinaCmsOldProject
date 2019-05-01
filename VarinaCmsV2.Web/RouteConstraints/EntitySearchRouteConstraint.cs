using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using VarinaCmsV2.Core.Contracts.DataServices;

namespace VarinaCmsV2.Web.RouteConstraints
{
    public class SchemeRouteConstraint:IRouteConstraint
    {
        private IEntitySchemeDataService _schemeData;
        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values,
            RouteDirection routeDirection)
        {
            _schemeData = Startup.Container.GetInstance<IEntitySchemeDataService>();
            var schemeUrl = values[parameterName].ToString();
            var existsScheme = _schemeData.Query.Any(x => x.Url == schemeUrl);
            return existsScheme;
        }
    }
}