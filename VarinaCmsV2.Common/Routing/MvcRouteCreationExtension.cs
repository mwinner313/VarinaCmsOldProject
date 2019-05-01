using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Properties;
using System.Web.Mvc.Routing;
using System.Web.Routing;

namespace VarinaCmsV2.Common.Routing
{
    public static class MvcRouteCreationExtension
    {
        public static void MapGreedyRouteWithName(this RouteCollection routes, string name, string url, object defaults, object constraints)
        {
            if (routes == null)
                throw new ArgumentNullException(nameof(routes));
            if (url == null)
                throw new ArgumentNullException(nameof(url));

            routes.Add(name, new GreedyRoute(
                url,
                new RouteValueDictionary(defaults),
                new RouteValueDictionary(constraints), null,
                new MvcRouteHandler())
            { DataTokens = new RouteValueDictionary { { "RouteName", name } } });
        }


        //public static Route MapRouteWithName(this RouteCollection routes,
        //    string name, string url, object defaults, object constraints = null)
        //{
        //    Route route = routes.MapRoute(name, url, defaults, constraints);
        //    route.DataTokens = new RouteValueDictionary { { "RouteName", name } };
        //    return route;
        //}
        public static void MapRouteWithName(this RouteCollection routes,
            string name, string url, object defaults, object constraints = null)
        {
            routes.Add(name, new SubdomainRoute(url)
            {
                Defaults = new RouteValueDictionary(defaults),
                Constraints = new RouteValueDictionary(constraints),
                DataTokens = new RouteValueDictionary { { "RouteName", name } }
            });

        }
    }
}
