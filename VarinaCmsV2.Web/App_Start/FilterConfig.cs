using System;
using System.Web;
using System.Web.Mvc;
using VarinaCmsV2.Common.MvcFilters;
using VarinaCmsV2.Core.Contracts.Configuration;
using VarinaCmsV2.Core.Web.Security;
using VarinaCmsV2.Core.Web.Security.Mvc;

namespace VarinaCmsV2.Web
{
    public static class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.AddAndInjectDependencies<TelegramErrorLoggerFilterAttribute>();
            filters.Add(new ElmahHandledErrorLoggerFilterAttribute());
            filters.Add(new HandleErrorAttribute());
            filters.Add(new CurrentCultureSetterFilterAttribute());
            filters.Add(new MvcCmsAuthorizeActionFilterAttribute());
        }

        public static void AddAndInjectDependencies<T>(this GlobalFilterCollection filters) where T : Attribute
        {
            var filter = Startup.Container.GetInstance<T>();
            filters.Add(filter);
        }
    }
}
