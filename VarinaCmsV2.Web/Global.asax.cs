using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Hosting;
using System.Web.Http.Owin;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.WebPages;
using Elmah;
using FluentScheduler;
using StructureMap.Web.Pipeline;
using VarinaCmsV2.Common.WebApi;
using VarinaCmsV2.IocCofig;

namespace VarinaCmsV2.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public T Resolve<T>() where T : class
        {
            return Startup.Container.GetInstance<T>();
        }
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configuration.Services.Replace(typeof(IHostBufferPolicySelector), new NoBufferMoreThan4MbPolicySelector());

           

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_PreRequestHandlerExecute(object sender, EventArgs e)
        {
         
        }
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            if(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority).Contains("localhost"))return;
            var leftPartOfUrl = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority).ToLower();
            if (leftPartOfUrl.StartsWith("http") && leftPartOfUrl.Split('.').Length == 1)
            {
                var fullUrl = HttpContext.Current.Request.Url.ToString();
                HttpContext.Current.Response.Status = "301 Moved Permanently";
                HttpContext.Current.Response.AddHeader("Location", fullUrl.Insert(fullUrl.IndexOf("://", StringComparison.Ordinal) + 3, "www."));
            }
        }
         protected void Application_End(object sender, EventArgs e)
        {
            JobManager.StopAndBlock();
        }
    }
}
