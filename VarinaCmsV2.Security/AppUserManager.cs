using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.DataProtection;
using VarinaCmsV2.Common;
using VarinaCmsV2.Data.Contracts;
using VarinaCmsV2.DomainClasses.Users;
using VarinaCmsV2.Security.Contracts;

namespace VarinaCmsV2.Security
{
    public class AppUserManager : UserManager<User, Guid>, IAppUserManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAppRoleManager _roleManager;
        private readonly IDbSet<User> _users;
        private readonly IDbSet<Role> _roles;
        private readonly Lazy<User> _currUser;

        public AppUserManager(IUserStore<User, Guid> store, IUnitOfWork unitOfWork, IAppRoleManager roleManager, IEmailSecuriyService emailSecuriyService) : base(store)
        {
            _unitOfWork = unitOfWork;
            _roleManager = roleManager;
            _users = _unitOfWork.Set<User>();
            _roles = _unitOfWork.Set<Role>();
            this.EmailService = emailSecuriyService;
            var provider = new DpapiDataProtectionProvider("Sample");
            this.UserTokenProvider = new DataProtectorTokenProvider<User, Guid>(
                provider.Create("EmailConfirmation"));
            UserLockoutEnabledByDefault = true;
            DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(10);
            MaxFailedAccessAttemptsBeforeLockout = 5;
            _currUser = new Lazy<User>(GetCurrentUser, false);
        }
        public Guid GetCurrentUserId()
        {
            var id = HttpContext.Current?.User?.Identity?.GetUserId();
            return string.IsNullOrEmpty(id) ? Guid.Empty : Guid.Parse(id);
        }

        #region Auxilarities

        public User FindById(Guid id, bool includeRoles = false)
        {
            if(includeRoles)
                return _users.Include(x=>x.Roles).First(x => x.Id == id);
            return _users.First(x => x.Id == id);
        }


        public async Task<User> FindByPhoneNumberAsUserNameAsync(string userName, string password)
        {
            var user = _users.FirstOrDefault(x => x.PhoneNumber == userName);
            if (user == null) return null;
            return await FindAsync(user.UserName, password);
        }
        public User GetCurrentUser()
        {
            var userId = GetCurrentUserId();
            return _users.Include(x => x.Roles).FirstOrDefault(x => x.Id == userId);
        }
        public async Task<User> GetCurrentUserAsync()
        {
            var userId = GetCurrentUserId();
            return await _users.Include(x => x.Roles).FirstOrDefaultAsync(x => x.Id == userId);
        }
        public async Task<bool> ExistsByUserNameAsync(string userName)
        {
            return await _users.AnyAsync(x => x.UserName == userName);
        }
      
        public bool CheckIsUserBanned(string userName)
        {
            return GetCurrentUser().IsBanned;
        }
      
        public bool HasCurrentUserAccsess(string action)
        {
            return GetCurrentUserPremissions().Any(x => x.Action == action);
        }

        public override Task<IdentityResult> CreateAsync(User user, string password)
        {
            string emailpattern = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";

            if (Regex.IsMatch(user.UserName, emailpattern) && string.IsNullOrEmpty(user.Email))
                user.Email = user.UserName;

            return base.CreateAsync(user, password);
        }

        public IEnumerable<Premission> GetUserPremissions(User user)
        {
            IEnumerable<Role> userroles = GetRoles(user);
            var premisions = userroles.SelectMany(x => x.PermissionsJObject).ToList();
            premisions.AddRange(user.AdditionalPermissions.ToList());
            return premisions.DistinctBy(x => x.Action).ToList();

        }

        public IQueryable<User> GetUsersInRole(string role)
        {
            var roleId = _roleManager.FindByName(role)?.Id;
            if (!roleId.HasValue) throw new ArgumentException("non existing role name specified");
            return _users.Where(x => x.Roles.Any(r => r.RoleId == roleId));
        }

        public IEnumerable<Role> GetRoles(User user)
        {
            var roles = (from userRole in user.Roles
                         join role in _roleManager.Roles.ToList() on userRole.RoleId equals role.Id
                         select role).ToList();
            return roles;
        }
        public IEnumerable<Role> GetRoles(Guid userId)
        {
            return GetRoles(FindById(userId, includeRoles: true));
        }
        private IEnumerable<Premission> GetCurrentUserPremissions()
        {
            return GetUserPremissions(_currUser.Value);
        }
        public bool ExistsByUrl(string userUrl)
        {
            return _users.Any(x => x.Url == userUrl);
        }

        public async Task<User> FindByUrlAsync(string userUrl)
        {
            return await _users.FirstOrDefaultAsync(x => x.Url == userUrl);
        }

        public List<User> GetAdminstrators()
        {
            var adminstratorRoleIds = new List<Guid?>
            {
                _roleManager.FindByName("PrincipalAdministrator")?.Id,
                _roleManager.FindByName("Administrator")?.Id,
                _roleManager.FindByName("Supervisor")?.Id
            };
            adminstratorRoleIds = adminstratorRoleIds.Where(x => x.HasValue).ToList();
            return _users.Where(x => x.Roles.Any(r => adminstratorRoleIds.Contains(r.RoleId))).ToList();
        }

        #endregion

        #region UserIdentity
        private async Task<ClaimsIdentity> GenerateUserIdentityAsync(AppUserManager manager, User user)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            var userroles = manager.GetRoles(user);
            // Add custom user claims here
            foreach (var role in userroles)
            {
                userIdentity.AddClaim(new Claim(ClaimTypes.Role, role.Name, "http://www.w3.org/2001/XMLSchema#string"));
            }
            foreach (Premission premission in manager.GetUserPremissions(user))
            {
                userIdentity.AddClaim(new Claim(CustomClaimTypes.Premission, premission.Action, "http://www.w3.org/2001/XMLSchema#string"));
            }
            return userIdentity;
        }

        public Func<CookieValidateIdentityContext, Task> OnValidateIdentity()
        {
            return CustomSecurityStampValidator.OnValidateIdentity(
                         validateInterval: TimeSpan.FromMinutes(15),
                         regenerateIdentityCallback: GenerateUserIdentityAsync,
                         getUserIdCallback: identity => Guid.Parse(identity.GetUserId()));
        }

        public override async Task<ClaimsIdentity> CreateIdentityAsync(User user, string authenticationType)
        {
            var identity = await base.CreateIdentityAsync(user, authenticationType);
            return identity;
        }
        #endregion

        #region DatabaseIntitalData
        public  void SeedDatabase(IUserManagerSeedDataBaseContext context)
        {
             CreateInitialUserIfNotExists(context.InitialUsers);
             CreateInitialRoleIfNotExists(context.InitialRoles);
             context.PostDataInitializeAsync(this);
        }
        private void CreateInitialRoleIfNotExists(List<Role> roles)
        {
            foreach (var role in roles)
            {
                if (! AsyncHelper.RunSync(()=>_roleManager.RoleExistsAsync(role.Name)))
                {

                    try
                    {
                        AsyncHelper.RunSync(() => _roleManager.CreateAsync(role));
                    }
                    catch (Exception e)
                    {
                        // ignored
                    }
                }
            }
        }
        private void CreateInitialUserIfNotExists(List<User> users)
        {
            foreach (var initalUser in users)
            {
                if (AsyncHelper.RunSync(() => ExistsByUserNameAsync(initalUser.UserName)))
                    continue;
                try
                {
                    var res = AsyncHelper.RunSync(() => CreateAsync(initalUser, initalUser.PasswordHash));
                    if (res == IdentityResult.Failed())
                        throw new InvalidOperationException(
                            $"New Initial User did not Created UserName={initalUser.UserName}");
                }
                catch (Exception exception)
                {
                    // ignored
                }
            }
        }



        #endregion
    }

}
