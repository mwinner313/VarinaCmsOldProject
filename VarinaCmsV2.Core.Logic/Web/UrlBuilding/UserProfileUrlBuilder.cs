using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using VarinaCmsV2.Core.Contracts.Web;
using VarinaCmsV2.Core.Logic.Helpers;
using VarinaCmsV2.Core.Web;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.DomainClasses.Users;

namespace VarinaCmsV2.Core.Logic.Web.UrlBuilding
{
    public class UserProfileUrlBuilder:ICmsUrlBuilder
    {
        private readonly UrlHelper _urlHelper;

        public UserProfileUrlBuilder()
        {
            _urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);
        }

        public string Generate(IUrlable item)
        {
            if(!(item is User user))throw new InvalidOperationException($"invalid or null argument in user profile url builder => {item}");
            return SettingHelper.MultiLanguageEnabled ? GenerateWithLang(user) : GenerateDefault(user);
        }

        private string GenerateWithLang(User user)
        {
            return _urlHelper.RouteUrl(Routes.UserProfileWithLang, new { userUrl = user.Url ,lang=Thread.CurrentThread.CurrentCulture.Name});
        }

        private string GenerateDefault(User user)
        {
            return _urlHelper.RouteUrl(Routes.UserProfile, new {userUrl = user.Url});
        }

        public string GenerateFull(IUrlable item)
        {
            return HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + Generate(item);
        }
    }
}
