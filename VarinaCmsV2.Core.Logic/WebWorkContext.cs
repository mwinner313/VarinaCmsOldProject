using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using VarinaCmsV2.Common;
using VarinaCmsV2.Common.Web;
using VarinaCmsV2.Core.Contracts;
using VarinaCmsV2.Core.Security.Premissions;
using VarinaCmsV2.Data.Contracts;
using VarinaCmsV2.DomainClasses.Users;
using VarinaCmsV2.Security.Contracts;

namespace VarinaCmsV2.Core.Logic
{
    public class WebWorkContext : IWorkContext
    {
        private readonly string _guestUserCookieName = "GuestUser-Id";
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAppUserManager _userManager;
        private readonly IAppRoleManager _appRoleManager;
        private User _cachedUser;
        public WebWorkContext(IAppUserManager userManager, IUnitOfWork unitOfWork, IAppRoleManager appRoleManager)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _appRoleManager = appRoleManager;
        }
        public User CurrentUser
        {
            get
            {
                if (_cachedUser != null) return _cachedUser;

                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    _cachedUser = GetUserById(HttpContext.Current.User.Identity.GetUserId().ToGuid());
                    return _cachedUser;
                }
                if (HttpContext.Current.Request.Cookies.AllKeys.Contains(_guestUserCookieName))
                {
                    _cachedUser = GetUserById(HttpContext.Current.Request.Cookies[_guestUserCookieName].ToString()
                        .ToGuid());
                    return _cachedUser;
                }
                _cachedUser = CreateGeuestUserAndAddCookieToRequest();
                return _cachedUser;
            }
        }

        private User GetUserById(Guid id)
        {
            return _unitOfWork.Set<User>().Include(x => x.ShoppingCartItems)
                        .Include(x => x.ShoppingCartItems.Select(s => s.Product))
                        .Include(x => x.ShoppingCartItems.Select(s => s.Product.Pictures))
                        .Include(x => x.Addresses)
                        .Include(x=>x.Attributes)
                        .Include(x => x.ShoppingCartItems.Select(s => s.Product.RequiredProducts)).FirstOrDefault(x => x.Id == id);
        }

        private User CreateGeuestUserAndAddCookieToRequest()
        {
            var user = new User()
            {
                CreateDateTime = DateTime.Now,
                UpdateDateTime = DateTime.Now,
                Id = Guid.NewGuid(),
                UserName = Guid.NewGuid().ToString().Replace("-",""),
            };

            var res = AsyncHelper.RunSync(() => _userManager.CreateAsync(user));
            if (!res.Succeeded)
            {
                throw new InvalidOperationException(string.Join(",", res.Errors));
            }

            AsyncHelper.RunSync(() => _userManager.AddToRoleAsync(user.Id, PreDefRoles.Guest));
            HttpContext.Current.Request.Cookies.Add(new HttpCookie(_guestUserCookieName) { Value = user.Id.ToString(), Expires = DateTime.Now.AddYears(1) });
            return user;
        }
    }
}
