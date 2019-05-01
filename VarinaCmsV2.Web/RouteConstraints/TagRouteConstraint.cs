using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using VarinaCmsV2.Core.Contracts.DataServices;

namespace VarinaCmsV2.Web.RouteConstraints
{
    public class TagRouteConstraint : IRouteConstraint
    {
        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values,
            RouteDirection routeDirection)
        {
            var tagsDataService = Startup.Container.GetInstance<ITagDataService>();
            var languageDataSrv = Startup.Container.GetInstance<ILanguageDataService>();
            var lang = PageRouteConstraint.GetLangFromRouteOrDefault(values);
            var tagUrl = values[parameterName].ToString();
            object value;
            if (values.TryGetValue("schemeUrl", out value))
            {
                var schemeUrl = value as string;
                return 
                    tagsDataService.Query.Any(
                        x => x.Url == tagUrl && x.Scheme.Url == schemeUrl) && languageDataSrv.FromCache().Any(x => x.Name == lang);
            }
            else
            {
                return languageDataSrv.FromCache().Any(x => x.Name == lang) &&
                    tagsDataService.Query.Any(
                        x => x.Url ==tagUrl &&x.LanguageName==lang) ;
            }
        }
    }
}