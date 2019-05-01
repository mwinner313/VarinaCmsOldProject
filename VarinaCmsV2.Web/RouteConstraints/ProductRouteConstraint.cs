using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.DomainClasses.Entities.Enums;

namespace VarinaCmsV2.Web.RouteConstraints
{
    public class ProductRouteConstraint : IRouteConstraint
    {
        private IProductDataService _products;
        public ProductRouteConstraint()
        {

        }

        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values,
            RouteDirection routeDirection)
        {
            _products = Startup.Container.GetInstance<IProductDataService>();
            var productUrl = values[parameterName].ToString();
            return _products.Query.Any(x => x.Url == productUrl && (x.VisibleIndividually || x.Type == ProductType.Grouped));
        }
    }
}