using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using VarinaCmsV2.Core.Contracts.Web;
using VarinaCmsV2.Core.Contracts.WebClientServices;
using VarinaCmsV2.Core.Contracts.WebClientServices.Entities;
using VarinaCmsV2.Core.Web;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.DomainClasses.Users;

namespace VarinaCmsV2.Core.Logic.Web.UrlBuilding
{
    class UserCreatedEntityUrlBuilder : ICmsUrlBuilder
    {
        private readonly UrlHelper _urlHelper;

        public UserCreatedEntityUrlBuilder()
        {
            _urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);
        }

        public string Generate(IUrlable item)
        {
            if (!(item is UserEntitiesPaginated userCreateds)) throw new InvalidOperationException($"null or invalid arg for page url builder {item}");
            return SettingHelper.MultiLanguageEnabled ? GenerateWithLang(userCreateds) : GenerateDefault(userCreateds);
        }
        public string Generate(UserEntitiesPaginated userCreateds , int pageNumber)
        {
            return SettingHelper.MultiLanguageEnabled ? GenerateWithLang(userCreateds, pageNumber) : GenerateDefault(userCreateds, pageNumber);
        }
        private string GenerateWithLang(UserEntitiesPaginated userCreateds, int pageNumber = 1)
        {
            return _urlHelper.RouteUrl(Routes.UserEntityRouteWithLang, new
            {
                userUrl = userCreateds.User.Url,
                schemeUrl = userCreateds.Scheme.Url,
                pageNumber,
                lang = Thread.CurrentThread.CurrentCulture.Name
            });
        }

        private string GenerateDefault(UserEntitiesPaginated userCreateds, int pageNumber = 1)
        {
            return _urlHelper.RouteUrl(Routes.UserEntityRoute, new
            {
                userUrl = userCreateds.User.Url,
                schemeUrl = userCreateds.Scheme.Url,
                pageNumber
            });
        }
        public string GenerateFull(IUrlable item)
        {
            return HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + Generate(item);
        }
    }
}
