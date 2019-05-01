using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using VarinaCmsV2.Core.Contracts.Web;
using VarinaCmsV2.Core.Web;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.DomainClasses.Entities.EShop;

namespace VarinaCmsV2.Core.Logic.Web.UrlBuilding
{
    public class ProductUrlBuilder : ICmsUrlBuilder
    {
        private readonly UrlHelper _urlHelper;

        public ProductUrlBuilder()
        {
            _urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);
        }

        public virtual string Generate(IUrlable item)
        {
            if (!(item is Product product)) throw new InvalidOperationException($"invalid argument passed to productUrlBuilder : {item}");
            return SettingHelper.MultiLanguageEnabled ? GenerateWithLang(product) : GenerateDefault(product);
        }

        private string GenerateWithLang(Product product)
        {
            var userSelectedRoute = WebConfigurationManager.AppSettings.Get("selected-product-route");
            return _urlHelper.RouteUrl(Routes.ProductWithLang,
                new { productUrl = product.Url, lang = Thread.CurrentThread.CurrentCulture.Name });
        }

        private string GenerateDefault(Product product)
        {
            var userSelectedRoute = WebConfigurationManager.AppSettings.Get("selected-product-route");
            return _urlHelper.RouteUrl(Routes.ProductDefault, new { productUrl = product.Url });
        }

        public virtual string GenerateFull(IUrlable item)
        {
            return HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + Generate(item);
        }
    }
}
