using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Routing;
using VarinaCmsV2.Core.Contracts.DataServices;
using Microsoft.AspNet.Identity.Owin;

namespace VarinaCmsV2.Web.RouteConstraints
{
    internal class EntityCategoryRouteConstraint : IRouteConstraint
    {
        private const string CategoryUrl = "categoryUrl";
        private const string SchemeUrl = "schemeUrl";
        public EntityCategoryRouteConstraint()
        {
        }

        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values,
            RouteDirection routeDirection)
        {
            var categoryDataSrv = Startup.Container.GetInstance<ICategoryDataService>();
            var categories = categoryDataSrv.Query.Include(x => x.Scheme).ToList();
            var schemeUrl = values[SchemeUrl];
            var catUrl = values[CategoryUrl];
            var lang = PageRouteConstraint.GetLangFromRouteOrDefault(values);
            var match = schemeUrl!=null&&catUrl!=null&&  categoryDataSrv.Query.Any(x =>
                  x.Url == catUrl.ToString() &&
                  x.Scheme.Url == schemeUrl.ToString()&&
                  x.LanguageName==lang
              );
            return match;
        }

       
    }
}