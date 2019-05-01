using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using VarinaCmsV2.Common;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.Core.Contracts.Security;
using VarinaCmsV2.Core.Infrastructure;
using VarinaCmsV2.Core.Security.Premissions;
using VarinaCmsV2.Core.Web.ActionFilters;
using VarinaCmsV2.Core.Web.Security;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.Security.Contracts;

namespace VarinaCmsV2.Core.Services
{
    public abstract class BaseService<T, TResponse, TListResponse> where T : class
                                                                             where TResponse : IServiceResponse, new()
                                                                             where TListResponse : IServiceResponse, new()
    {
        private readonly List<IBaseBeforeAddingEntityLogic> _baseBeforeAddingEntityLogics;
        private readonly List<BaseAfterAddingEntityLogic> _baseAfterAddingEntityLogics;
        private readonly List<IBaseBeforeUpdatingEntityLogic> _baseBeforeUpdateEntityLogics;
        private readonly List<IBaseAfterUpdatingEntityLogic> _baseAfterUpdateEntityLogics;
        private readonly List<IBaseBeforeDeleteEntityLogic> _baseBeforeDeleteEntityLogics;
        private readonly List<BaseAfterDeleteEntityLogic> _baseAfterDeleteEntityLogics;
        private readonly IIdentityManager _identityManager;
        protected readonly IRestrictedItemAccessManager AccessManager;
        private readonly IDataService<T, Guid> _dataSrv;
        protected BaseService(
            List<IBaseBeforeAddingEntityLogic> baseBeforeAddingEntityLogics,
            List<BaseAfterAddingEntityLogic> baseAfterAddingEntityLogics,
            List<IBaseAfterUpdatingEntityLogic> baseAfterUpdateEntityLogics,
            List<IBaseBeforeUpdatingEntityLogic> baseBeforeUpdateEntityLogics,
            List<IBaseBeforeDeleteEntityLogic> baseBeforeDeleteEntityLogics,
            List<BaseAfterDeleteEntityLogic> baseAfterDeleteEntityLogics,
            IIdentityManager identityManager,
            IRestrictedItemAccessManager accessManager, IDataService<T, Guid> dataSrv)
        {
            _baseBeforeAddingEntityLogics = baseBeforeAddingEntityLogics;
            _baseAfterAddingEntityLogics = baseAfterAddingEntityLogics;
            _baseAfterUpdateEntityLogics = baseAfterUpdateEntityLogics;
            _baseBeforeUpdateEntityLogics = baseBeforeUpdateEntityLogics;
            _baseBeforeDeleteEntityLogics = baseBeforeDeleteEntityLogics;
            _baseAfterDeleteEntityLogics = baseAfterDeleteEntityLogics;
            _identityManager = identityManager;
            AccessManager = accessManager;
            _dataSrv = dataSrv;
        }



        protected bool HasPremission(IPrincipal principal, string premission)
        {
            return _identityManager.HasPremission(principal, premission);
        }


        protected virtual bool HasAccessToManage(object item, IPrincipal principal)
        {
            if (principal.IsInRole(PreDefRoles.Supervisor) || principal.IsInRole(PreDefRoles.PrincipalAdministrator))
                return true;

            return !(item is IAccessRestrictedItem) || AccessManager.HasAccess((IAccessRestrictedItem)item, AccessPremission.Manage);
        }

        protected bool HasAccessToSee(object item, IPrincipal principal)
        {
            if (principal.IsInRole(PreDefRoles.Supervisor) || principal.IsInRole(PreDefRoles.PrincipalAdministrator))
                return true;

            return !(item is IAccessRestrictedItem) || AccessManager.HasAccess((IAccessRestrictedItem)item, AccessPremission.See);
        }

        protected async Task BaseBeforeAddAsync(T obj, IPrincipal requestOwner)
        {
            await _baseBeforeAddingEntityLogics.ForEachAsync(x => x.ApplyLogicAsync(obj, requestOwner));
        }
        protected async Task BaseAfterAddAsync(T obj, IPrincipal requestOwner)
        {
            await _baseAfterAddingEntityLogics.ForEachAsync(x => x.ApplyLogicAsync(obj, requestOwner));
        }

        protected async Task BaseBeforeUpdateAsync(T obj, IPrincipal requestOwner)
        {
            await _baseBeforeUpdateEntityLogics.ForEachAsync(x => x.ApplyLogicAsync(obj, requestOwner));
        }
        protected async Task BaseAfterUpdateAsync(T obj, IPrincipal requestOwner)
        {
            await _baseAfterUpdateEntityLogics.ForEachAsync(x => x.ApplyLogicAsync(obj, requestOwner));
        }

        protected async Task BaseBeforeDeleteAsync(T obj, IPrincipal requestOwner)
        {
            await _baseBeforeDeleteEntityLogics.ForEachAsync(x => x.ApplyLogicAsync(obj, requestOwner));
        }
        protected async Task BaseAfterDeleteAsync(T obj, IPrincipal requestOwner)
        {
            await _baseAfterDeleteEntityLogics.ForEachAsync(x => x.ApplyLogicAsync(obj, requestOwner));
        }
        protected async Task BaseAfterDeleteAsync(object obj, IPrincipal requestOwner)
        {
            await _baseAfterDeleteEntityLogics.ForEachAsync(x => x.ApplyLogicAsync(obj, requestOwner));
        }
        protected TResponse UnauthorizedRequest(string message = null)
        {
            return new TResponse()
            {
                Access = ResponseAccess.Deny,
                Message = message
            };
        }
        protected TListResponse UnauthorizedListRequest(string message = null)
        {
            return new TListResponse()
            {
                Access = ResponseAccess.Deny,
                Message = message
            };
        }
        protected TResponse Success(string message = "")
        {
            return new TResponse()
            {
                Access = ResponseAccess.Granted,
                Message = message
            };
        }
    }

}