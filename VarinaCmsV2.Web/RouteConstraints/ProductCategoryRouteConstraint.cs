using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.Core.Services.ProductCategories;

namespace VarinaCmsV2.Web.RouteConstraints
{
    public class ProductCategoryRouteConstraint : IRouteConstraint
    {

        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values,
            RouteDirection routeDirection)
        {
            var url = values[parameterName].ToString();
            return Startup.Container.GetInstance<IProductCategoryDataService>().Query.Any(x => x.Url == url);
        }
    }
}