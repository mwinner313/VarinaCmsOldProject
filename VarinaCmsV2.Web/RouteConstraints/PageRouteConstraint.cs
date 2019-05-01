using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Routing;
using VarinaCmsV2.Core.Contracts.DataServices;

namespace VarinaCmsV2.Web.RouteConstraints
{
    public class PageRouteConstraint : IRouteConstraint
    {

        public PageRouteConstraint()
        {
        }

        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values,
            RouteDirection routeDirection)
        {
            var pageDataService = Startup.Container.GetInstance<IPageDataService>();
            object rawUrl = new object();

            values.TryGetValue(parameterName, out rawUrl);
            if (rawUrl != null)
            {
                var urlString = rawUrl.ToString();
                var lang = GetLangFromRouteOrDefault(values);
                return pageDataService.Query.Any(x => x.Url == urlString&&x.LanguageName==lang);
            }
            return false;
        }

        public static string GetLangFromRouteOrDefault(RouteValueDictionary values)
        {
            object lang = new object();
            var languageDataSrv = Startup.Container.GetInstance<ILanguageDataService>();
            values.TryGetValue("lang", out lang);
            if (lang != null && languageDataSrv.FromCache().Any(x => x.Name == lang.ToString()))
            {
                return lang.ToString();
            }

            return WebConfigurationManager.AppSettings.Get("default-language");
        }
    }
}