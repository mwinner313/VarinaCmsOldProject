using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using VarinaCmsV2.Common;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.Core.Contracts.Security;
using VarinaCmsV2.Core.Infrastructure;
using VarinaCmsV2.Core.Logic.Helpers;
using VarinaCmsV2.Core.Security.Premissions;
using VarinaCmsV2.Core.Services;
using VarinaCmsV2.Core.Services.UserManagement;
using VarinaCmsV2.DomainClasses.Users;
using VarinaCmsV2.Security.Contracts;

namespace VarinaCmsV2.Core.Logic.Services.UserManagement
{
    public class UserManagementService : BaseService<User, UserResponse, UserListResponse>, IUserManagementService
    {
        private readonly IUserDataService _userDataSrv;
        private readonly IAppRoleManager _roleManager;
        private readonly IAppUserManager _userManager;
        private readonly IUserFilterStrategyFactory _userFilterStrategyFactory;
        private readonly ISecurityLogger _securityLogger;

        public UserManagementService(List<IBaseBeforeAddingEntityLogic> baseBeforeAddingEntityLogics,
            List<BaseAfterAddingEntityLogic> baseAfterAddingEntityLogics,
            List<IBaseAfterUpdatingEntityLogic> baseAfterUpdateEntityLogics,
            List<IBaseBeforeUpdatingEntityLogic> baseBeforeUpdateEntityLogics,
            List<IBaseBeforeDeleteEntityLogic> baseBeforeDeleteEntityLogics,
            List<BaseAfterDeleteEntityLogic> baseAfterDeleteEntityLogics, IIdentityManager identityManager,
            IRestrictedItemAccessManager accessManager, IUserDataService userDataSrv,
            IUserFilterStrategyFactory userFilterStrategyFactory, ISecurityLogger securityLogger,
            IAppRoleManager roleManager, IAppUserManager userManager)
            : base(
                baseBeforeAddingEntityLogics, baseAfterAddingEntityLogics, baseAfterUpdateEntityLogics,
                baseBeforeUpdateEntityLogics, baseBeforeDeleteEntityLogics, baseAfterDeleteEntityLogics, identityManager,
                accessManager, userDataSrv)
        {
            _userDataSrv = userDataSrv;
            _userFilterStrategyFactory = userFilterStrategyFactory;
            _securityLogger = securityLogger;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<UserResponse> GetAsync(UserGetRequest request)
        {
            if (!HasPremission(request.RequestOwner, UserManagePremission.CanSeeUsers)) return UnauthorizedRequest();

            var user = await _userDataSrv.Query.FirstAsync(x => x.Id == request.UserId);
            if (!HasAccessToSee(user, request.RequestOwner)) return UnauthorizedRequest();

            return new UserResponse() {Access = ResponseAccess.Granted, User = user.MapToViewModel()};
        }

        public async Task<UserListResponse> GetAsync(UserListRequest request)
        {
            if (!HasPremission(request.RequestOwner, UserManagePremission.CanSeeUsers))
                return UnauthorizedListRequest();
            var guestRoleId = _roleManager.FindByName(PreDefRoles.Guest).Id;
            var userQuery = AccessManager.Filter(_userDataSrv.Query).FilterByQuery(request.Query).Where(x => x.Roles.All(r => r.RoleId != guestRoleId));
            userQuery = _userFilterStrategyFactory.GetStrategy(request.RequestOwner.Identity).Filter(userQuery);
            var currentUserId = Guid.Parse(request.RequestOwner.Identity.GetUserId());
            return new UserListResponse()
            {
                Access = ResponseAccess.Granted,
                Users =
                    await userQuery.OrderByDescending(x => x.Id)
                        .Where(x => x.Id != currentUserId )
                        .Paginate(request.Query ?? new Pagenation())
                        .ToViewModelListAsync(),
                Count = userQuery.Where(x => x.Id != currentUserId).LongCount()
            };
        }

        public async Task<UserResponse> AddAsync(UserAddRequest request)
        {
            if (!HasPremission(request.RequestOwner, UserManagePremission.CanAdd))
            {
                _securityLogger.LogDangeriousAddAttemp(request.RequestOwner, request.User);
                return UnauthorizedRequest();
            }
            var newUser = request.User.MapToModel();
            newUser.Id = Guid.NewGuid();
            await BaseBeforeAddAsync(newUser, request.RequestOwner);
            var res = await _userManager.CreateAsync(newUser, request.User.Password);
            if (!res.Succeeded)
                return new UserResponse()
                {
                    Access = ResponseAccess.BadRequest,
                    Message = string.Join(",", res.Errors)
                };
            await BaseAfterAddAsync(newUser, request.RequestOwner);
            return Success();
        }

        public async Task<UserResponse> UpdateAsync(UserUpdateRequest request)
        {
            var updatingUser =  _userManager.FindById(request.UserId,includeRoles:true);

            if (!HasPremission(request.RequestOwner, UserManagePremission.CanUpdate) || 
                !HasAccessToManage(updatingUser, request.RequestOwner))
            {
                _securityLogger.LogDangeriousUpdateAttemp(request.RequestOwner, updatingUser);
                return UnauthorizedRequest();
            }
            updatingUser.Roles.Clear();
            request.User.MapToModel(updatingUser);
            await BaseBeforeUpdateAsync(updatingUser, request.RequestOwner);
            var res = await _userManager.UpdateAsync(updatingUser);
            if (!res.Succeeded)
                return new UserResponse()
                {
                    Access = ResponseAccess.BadRequest,
                    Message = string.Join(",", res.Errors)
                };
            await BaseAfterUpdateAsync(updatingUser, request.RequestOwner);
            return new UserResponse() {Access = ResponseAccess.Granted};
        }

       

        public async Task<UserResponse> DeleteAsync(UserDeleteRequest request)
        {
            var deletingUser = await _userDataSrv.GetAsync(request.UserId);

            if (!HasPremission(request.RequestOwner, UserManagePremission.CanDelete) || 
                !HasAccessToManage(deletingUser, request.RequestOwner))
            {
                _securityLogger.LogDangeriousDeleteAttemp(request.RequestOwner, deletingUser);
                return UnauthorizedRequest();
            }
            await BaseBeforeDeleteAsync(deletingUser, request.RequestOwner);
            await _userDataSrv.DeleteAsync(deletingUser.Id);
            await BaseAfterDeleteAsync(deletingUser, request.RequestOwner);
            return Success();
        }


        protected override bool HasAccessToManage(object item, IPrincipal principal)
        {
            if (principal.IsInRole(PreDefRoles.Supervisor) || principal.IsInRole(PreDefRoles.PrincipalAdministrator))
                return true;
            if (!principal.IsInRole(PreDefRoles.Administrator)) return false;

            var itemRoles = _userManager.GetRoles(item as User);
            return
                itemRoles.Any(x => x.Name != PreDefRoles.Administrator || x.Name != PreDefRoles.PrincipalAdministrator) &&
                base.HasAccessToManage(item, principal);
        }
    }
}