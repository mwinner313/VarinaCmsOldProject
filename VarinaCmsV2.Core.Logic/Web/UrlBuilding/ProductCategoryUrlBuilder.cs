using System;
using System.Threading;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using VarinaCmsV2.Core.Contracts.Web;
using VarinaCmsV2.Core.Web;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.DomainClasses.Entities.EShop;

namespace VarinaCmsV2.Core.Logic.Web.UrlBuilding
{
    internal class ProductCategoryUrlBuilder : ICmsUrlBuilder
    {
        private readonly UrlHelper _urlHelper;

        public ProductCategoryUrlBuilder()
        {
            _urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);
        }

        public string Generate(IUrlable item)
        {
            if (!(item is ProductCategory category)) throw new InvalidOperationException($"invalid argument passed to ProductCategoryUrlBuilder : {item}");
            return SettingHelper.MultiLanguageEnabled ? GenerateWithLang(category, 1) : GenerateDefault(category, 1);
        }
        public string Generate(IUrlable item, int pageNumber)
        {
            if (!(item is ProductCategory category)) throw new InvalidOperationException($"invalid argument passed to ProductCategoryUrlBuilder : {item}");
            return SettingHelper.MultiLanguageEnabled ? GenerateWithLang(category, pageNumber) : GenerateDefault(category, pageNumber);
        }
        private string GenerateWithLang(ProductCategory cat, int pageNumber)
        {
            var userSelectedRoute = WebConfigurationManager.AppSettings.Get("selected-product-category-route");
            return _urlHelper.RouteUrl(Routes.ProductCategoryWithLang,
                new { productCategoryUrl = cat.Url, lang = Thread.CurrentThread.CurrentCulture.Name, pageNumber });
        }

        private string GenerateDefault(ProductCategory cat, int pageNumber)
        {
            var userSelectedRoute = WebConfigurationManager.AppSettings.Get("selected-product-category-route");
            return _urlHelper.RouteUrl(Routes.ProductCategoryDefault, new { productCategoryUrl = cat.Url, pageNumber });
        }
        public string GenerateFull(IUrlable item)
        {
            return HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + Generate(item);
        }
    }
}