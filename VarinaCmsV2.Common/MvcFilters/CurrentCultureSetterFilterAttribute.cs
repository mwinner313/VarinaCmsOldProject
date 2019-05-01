using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web.Configuration;
using System.Web.Mvc;

namespace VarinaCmsV2.Common.MvcFilters
{
    public class CurrentCultureSetterFilterAttribute:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var currentRequestCulture = filterContext.RouteData.Values?["lang"]?.ToString();
            if (string.IsNullOrEmpty(currentRequestCulture)|| CultureInfo.GetCultures(CultureTypes.AllCultures).All(x => x.Name != currentRequestCulture))
            {
                currentRequestCulture = WebConfigurationManager.AppSettings["default-language"];
            }
            Thread.CurrentThread.CurrentCulture = new CultureInfo(currentRequestCulture);
            base.OnActionExecuting(filterContext);
        }
    }
}
