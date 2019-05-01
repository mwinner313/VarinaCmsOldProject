using System;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using VarinaCmsV2.Core.Contracts.Web;
using VarinaCmsV2.Core.Logic.Helpers;
using VarinaCmsV2.Core.Web;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.DomainClasses.Entities;

namespace VarinaCmsV2.Core.Logic.Web.UrlBuilding
{
    public class EntityUrlBuilder : ICmsUrlBuilder
    {
        private readonly UrlHelper _urlHelper;

        public EntityUrlBuilder()
        {
            _urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);
        }

        public string Generate(IUrlable item)
        {
            if (!(item is Entity entity)) throw new InvalidOperationException($"invalid argument passed to entityUrlBuilder : {item}");

            return SettingHelper.MultiLanguageEnabled ? GenerateWithLang(entity) : GenerateDefault(entity);
        }

        private string GenerateDefault(Entity entity)
        {
            var userSelectedRoute = WebConfigurationManager.AppSettings.Get("selected-entity-route");

            if (userSelectedRoute == Routes.EntityWithDate)
                return _urlHelper.RouteUrl(Routes.EntityWithDate,
                    new { entityUrl = entity.Url, schemeUrl = entity.Scheme.Url, year = entity.PublishDateTime.Year, day = entity.PublishDateTime.Day, month = entity.PublishDateTime.Month });

            return _urlHelper.RouteUrl(Routes.EntityDefault,
                      new { entityUrl = entity.Url, schemeUrl = entity.Scheme.Url });
        }

        private string GenerateWithLang(Entity entity)
        {
            var userSelectedRoute = WebConfigurationManager.AppSettings.Get("selected-entity-route");


            if (userSelectedRoute == Routes.EntityWithLangAndDate)
                return _urlHelper.RouteUrl(Routes.EntityWithLangAndDate,
                    new { entityUrl = entity.Url, schemeUrl = entity.Scheme.Url, lang = entity.LanguageName, year = entity.PublishDateTime.Year, day = entity.PublishDateTime.Day, month = entity.PublishDateTime.Month });

            return _urlHelper.RouteUrl(Routes.EntityWithLang,
               new { entityUrl = entity.Url, schemeUrl = entity.Scheme.Url, lang = entity.LanguageName });
        }

        public string GenerateFull(IUrlable item)
        {
            return HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + Generate(item);
        }
    }
}