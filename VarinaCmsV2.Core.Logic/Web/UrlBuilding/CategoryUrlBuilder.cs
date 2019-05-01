using System;
using System.Web;
using System.Web.Mvc;
using VarinaCmsV2.Core.Contracts.Web;
using VarinaCmsV2.Core.Web;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.DomainClasses.Entities;

namespace VarinaCmsV2.Core.Logic.Web.UrlBuilding
{
    public class CategoryUrlBuilder : ICmsUrlBuilder
    {
        private readonly UrlHelper _urlHelper;

        public CategoryUrlBuilder()
        {
            _urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);
        }

        public string Generate(IUrlable item)
        {
            if (!(item is Category category)) throw new InvalidOperationException($"invalid or null argument in categuryUrlBuilder =>{item}");
            return SettingHelper.MultiLanguageEnabled ? GenerateWithLang(category,pageNumber: 1) : GenerateDefault(category, pageNumber: 1);
        }

        public string Generate(Category category, int pageNumber = 1)
        {
            return SettingHelper.MultiLanguageEnabled ? GenerateWithLang(category, pageNumber) : GenerateDefault(category, pageNumber);
        }
        private string GenerateWithLang(Category category, int pageNumber)
        {
            return _urlHelper.RouteUrl(Routes.EntityCategoryWithLang,
               new { categoryUrl = category.Url, schemeUrl = category.Scheme.Url, lang = category.LanguageName, pageNumber = pageNumber });

        }

        private string GenerateDefault(Category category, int pageNumber)
        {
            return _urlHelper.RouteUrl(Routes.EntityCategory,
                new { categoryUrl = category.Url, schemeUrl = category.Scheme.Url, pageNumber });

        }

        public string GenerateFull(IUrlable item)
        {
            return HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + Generate(item);
        }
    }
}