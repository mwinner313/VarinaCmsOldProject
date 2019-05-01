using System;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using DotLiquid;
using VarinaCmsV2.Core.Contracts.Web;
using VarinaCmsV2.Core.Logic.Helpers;
using VarinaCmsV2.Core.Web;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.DomainClasses.Entities;
using Tag = VarinaCmsV2.DomainClasses.Entities.Tag;

namespace VarinaCmsV2.Core.Logic.Web.UrlBuilding
{
    public class TagUrlBuilder : ICmsUrlBuilder
    {
        private readonly UrlHelper _urlHelper;

        public TagUrlBuilder()
        {
            _urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);
        }

        public string Generate(IUrlable item)
        {
            if (!(item is Tag tag)) throw new InvalidOperationException($"invalid or null argument => {item}");
            //TODO write custom entitytagroutes 
            //var userSelectedTagRoute = WebConfigurationManager.AppSettings.Get("selected-entitytag-route");

            return SettingHelper.MultiLanguageEnabled ? GenerateWithLang(tag) : GenerateDefault(tag);
        }

        private string GenerateDefault(Tag tag, int pageNumber = 1)
        {
            return _urlHelper.RouteUrl(Routes.Tag, new
            {
                tagUrl = tag.Url,
                pageNumber
            });
        }

        private string GenerateWithLang(Tag tag, int pageNumber = 1)
        {
            return _urlHelper.RouteUrl(Routes.TagWithLang, new
            {
                tagUrl = tag.Url,
                lang = Thread.CurrentThread.CurrentCulture.Name,
                pageNumber
            });
        }

        public string GenerateFull(IUrlable item)
        {
            return HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + Generate(item);
        }

        internal string Generate(Tag tag, int pageNumber = 1)
        {
            return SettingHelper.MultiLanguageEnabled ? GenerateWithLang(tag, pageNumber) : GenerateDefault(tag, pageNumber);
        }
    }
}