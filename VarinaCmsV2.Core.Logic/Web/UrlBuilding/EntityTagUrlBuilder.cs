using System;
using System.Threading;
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
    public class EntityTagUrlBuilder : ICmsUrlBuilder
    {
        private readonly UrlHelper _urlHelper;

        public EntityTagUrlBuilder()
        {
            _urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);
        }

        public string Generate(IUrlable item)
        {
            var tag = item as Tag;
            if (tag == null) throw new InvalidOperationException($"invalid or null argument =>{item}");
            //TODO write custom entitytagroutes 
            //var userSelectedTagRoute = WebConfigurationManager.AppSettings.Get("selected-entitytag-route");

            return SettingHelper.MultiLanguageEnabled ? GenerateWithLang(tag) : GenerateDefault(tag);
        }

        internal string Generate(Tag tag, int pageNumber = 1)
        {
            return SettingHelper.MultiLanguageEnabled ? GenerateWithLang(tag, pageNumber) : GenerateDefault(tag, pageNumber);
        }

        private string GenerateDefault(Tag tag, int pageNumber = 1)
        {
          var x  = _urlHelper.RouteUrl(Routes.EntityTag, new
            {
                schemeUrl = tag.Scheme?.Url,
                tagUrl = tag.Url,
                pageNumber
            });
            return x;
        }

        private string GenerateWithLang(Tag tag,int pageNumber=1)
        {
            return _urlHelper.RouteUrl(Routes.EntityTagWithLang, new
            {
                schemeUrl = tag.Scheme?.Url,
                tagUrl = tag.Url,
                lang = tag.LanguageName,
                pageNumber
            });
        }

        public string GenerateFull(IUrlable item)
        {
            return HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + Generate(item);
        }
    }
}