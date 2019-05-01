using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace VarinaCmsV2.Common.WebApi
{
    public class CurrentCultureSetterFilterAttribute : ActionFilterAttribute
    {
        public override Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            SetCurrentCulture(actionContext);
            return base.OnActionExecutingAsync(actionContext, cancellationToken);
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            SetCurrentCulture(actionContext);
            base.OnActionExecuting(actionContext);
        }

        private static void SetCurrentCulture(HttpActionContext actionContext)
        {
            var queryString = actionContext.Request
                .GetQueryNameValuePairs()
                .ToDictionary(x => x.Key, x => x.Value);
            var currentRequestCulture = "";
            queryString.TryGetValue("languageName", out currentRequestCulture);

            if (string.IsNullOrEmpty(currentRequestCulture) ||
                CultureInfo.GetCultures(CultureTypes.AllCultures).All(x => x.Name != currentRequestCulture))
            {
                currentRequestCulture = WebConfigurationManager.AppSettings["default-language"];
            }
            Thread.CurrentThread.CurrentCulture = new CultureInfo(currentRequestCulture);
        }
    }
}
