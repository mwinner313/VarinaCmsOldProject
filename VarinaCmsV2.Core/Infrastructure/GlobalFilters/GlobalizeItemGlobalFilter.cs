using System.Data.Entity;
using System.Web;
using System.Web.Configuration;
using EntityFramework.DynamicFilters;
using VarinaCmsV2.Common;
using VarinaCmsV2.DomainClasses.Contracts;

namespace VarinaCmsV2.Core.Infrastructure.GlobalFilters
{
    public class GlobalizeItemGlobalFilter : GlobalFilter
    {
        public override void ApplyFilter(DbModelBuilder modelBuilder)
        {
            modelBuilder.Filter("Globalized_items_baseon_language_filter", (IGlobalizedItem item, string langName) => item.LanguageName == langName, GetCurrentLang);
        }

        private string GetCurrentLang()
        {


            var lang = HttpContext.Current != null && HttpContext.Current.Items.Contains("lang") ? HttpContext.Current.Items["lang"] as string : null;
            if (lang != null)
                return lang;

            if (HttpContext.Current != null && !string.IsNullOrEmpty(HttpContext.Current.Request.Params["languageName"]))
                return HttpContext.Current.Request.Params["languageName"];

            var currentLang = WebConfigurationManager.AppSettings.Get("default-language");
            return currentLang;
        }
    }
}