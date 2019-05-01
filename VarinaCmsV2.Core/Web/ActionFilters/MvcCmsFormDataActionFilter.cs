using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;
using StructureMap;
using VarinaCmsV2.Common;
using VarinaCmsV2.Common.CustomFields;
using VarinaCmsV2.Common.MVC;
using VarinaCmsV2.Core.Contracts;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.DomainClasses.Entities;

namespace VarinaCmsV2.Core.Web.ActionFilters
{
    public class MvcCmsFormDataActionFilter : ActionFilterAttribute
    {
        public static Func<IContainer> GetContainer { get; set; }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var errors = new List<string>();
            var fieldFactroyProvider = GetContainer().GetInstance<CustomFieldFactoryProvider<JObject>>();
            var formHandle = filterContext.RouteData.Values["formHandle"].ToString();
            var formScheme = GetFormScheme(filterContext);
            if (formScheme == null)
            {
                filterContext.Result = new CustomJsonResult() { Data = new { message = $"no form with name of {formHandle} exists.", },StatusCode = 400};
                return;
            }

            foreach (string prop in filterContext.HttpContext.Request.Form)
            {
                var fd = formScheme.FieldDefenitions.FirstOrDefault(x => x.Handle == prop);
                if (fd == null) continue;
                if (
                    !fieldFactroyProvider.GetFieldFactory(fd.Type)
                        .IsValidForField(filterContext.HttpContext.Request.Form[prop],fd.MetaData))
                    errors.Add($"invalid value for field : {prop} => {filterContext.HttpContext.Request.Form[prop]}");
            }

            foreach (string prop in filterContext.HttpContext.Request.Files)
            {
                var fd = formScheme.FieldDefenitions.FirstOrDefault(x => x.Handle == prop);
                if (fd == null) continue;
                if (
                    !fieldFactroyProvider.GetFieldFactory(fd.Type)
                        .IsValidForField(new FileFieldDataWrapper(filterContext.HttpContext.Request.Files[prop]), fd.MetaData))
                    errors.Add($"invalid value for field : {prop} => {filterContext.HttpContext.Request.Files[prop]}");
            }
            if (errors.Count != 0) filterContext.Result = new CustomJsonResult() { Data = new { errors = errors } ,StatusCode = 400};
            else
            {
                base.OnActionExecuting(filterContext);
            }
        }

        private FormScheme GetFormScheme(ActionExecutingContext filterContext)
        {
            var fsDataSrv = GetContainer().GetInstance<IFormSchemeDataService>();
            var formHandle = filterContext.RouteData.Values["formHandle"].ToString();
            return fsDataSrv.Query.Include(x => x.FieldDefenitions).FirstOrDefault(x => x.Handle == formHandle);
        }
    }
}

