using System;
using System.Threading;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using VarinaCmsV2.Core.Contracts.Web;
using VarinaCmsV2.Core.Web;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.DomainClasses.Entities;

namespace VarinaCmsV2.Core.Logic.Web.UrlBuilding
{
    public class PageUrlBuilder : ICmsUrlBuilder
    {
        private readonly UrlHelper _urlHelper;

        public PageUrlBuilder()
        {
            _urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);
        }

        public string Generate(IUrlable item)
        {
            if (!(item is Page page)) throw new InvalidOperationException($"null or invalid arg for page url builder {item}");

            //var userSelectedRoute = WebConfigurationManager.AppSettings.Get("selected-page-route");
            return SettingHelper.MultiLanguageEnabled ? GenerageWithLang(page) : _urlHelper.RouteUrl(Routes.Page,new {pageUrl=page.Url});
        }

        private string GenerageWithLang(Page page)
        {
            return _urlHelper.RouteUrl(Routes.PageWithLang, new { pageUrl = page.Url,lang=page.LanguageName });
        }

        public string GenerateFull(IUrlable item)
        {
            return HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + Generate(item);
        }
    }
}