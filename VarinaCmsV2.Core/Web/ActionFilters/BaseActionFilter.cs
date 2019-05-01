using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;

namespace VarinaCmsV2.Core.Web.ActionFilters
{
    public class BaseActionFilter:ActionFilterAttribute
    {
        protected T Resolve<T>() where T : class
        {
            return DependencyResolver.Current.GetService<T>()??
                   HttpContext.Current.GetOwinContext().Get<T>()
                   ;
        }
    }
}
