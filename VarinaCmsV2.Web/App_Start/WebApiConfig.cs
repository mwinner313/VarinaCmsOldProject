using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using VarinaCmsV2.Common.WebApi;
using VarinaCmsV2.Common.WebApiFilters;
using VarinaCmsV2.Core.Web.Security.WebApi;
using VarinaCmsV2.Web.DependencyResolvers;
using VarinaCmsV2.Web.Infrastructure;

namespace VarinaCmsV2.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.Services.Replace(typeof(IHttpActionInvoker), new ApiInjectingActionInvoker(Startup.Container));
            // Web API routes
            config.MapHttpAttributeRoutes();
            config.Formatters.RemoveAt(0);

            config.Filters.Add(new WebApiCmsAuthorizeActionFilterAttribute());
            config.Filters.AddAndInjectDependencies<TelegramErrorLoggerFilterAttribute>();
            config.Filters.Add(new ElmahHandledErrorLoggerFilterAttribute());
            config.Filters.Add(new ValidationActionFilter());
            config.Filters.Add(new CurrentCultureSetterFilterAttribute());
            config.DependencyResolver = new WebApiDependecyResolver(Startup.Container);
            config.Formatters.Insert(0, new SecureAngularJsMediaTypeFormatter());
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
                name: "DefaultApiValidation",
                routeTemplate: "api/{controller}/{action}"
            );
            WebApiCmsAuthorize.GetContainer = () => Startup.Container;

            JsonConvert.DefaultSettings = () => new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
        }

        public static void AddAndInjectDependencies<T>(this HttpFilterCollection filters) where T : IFilter
        {
            var filter = Startup.Container.GetInstance<T>();
            Startup.Container.BuildUp(filter);
            filters.Add(filter);
        }
    }
}
