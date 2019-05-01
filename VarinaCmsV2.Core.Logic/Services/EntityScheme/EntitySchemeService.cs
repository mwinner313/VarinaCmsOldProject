using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using VarinaCmsV2.Common;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.Core.Contracts.Security;
using VarinaCmsV2.Core.Infrastructure;
using VarinaCmsV2.Core.Logic.Helpers;
using VarinaCmsV2.Core.LogicFacade;
using VarinaCmsV2.Core.Security.Premissions;
using VarinaCmsV2.Core.Services;
using VarinaCmsV2.Core.Services.EntityScheme;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.Security.Contracts;
using VarinaCmsV2.DomainClasses.Entities;
namespace VarinaCmsV2.Core.Logic.Services.EntityScheme
{
    public class EntitySchemeService : BaseService<DomainClasses.Entities.EntityScheme, EntitySchemeResponse, EntitySchemeListResponse>, IEntitySchemeService
    {
        private readonly IEntitySchemeDataService _dataService;
        private readonly IEntitySchemeFacade _schemeFacade;
        private readonly IEntityDataService _entityDataService;
        private readonly ICategoryDataService _categoryDataService;
        public EntitySchemeService(List<IBaseBeforeAddingEntityLogic> baseBeforeAddingEntityLogics, List<BaseAfterAddingEntityLogic> baseAfterAddingEntityLogics, List<IBaseAfterUpdatingEntityLogic> baseAfterUpdateEntityLogics, List<IBaseBeforeUpdatingEntityLogic> baseBeforeUpdateEntityLogics, List<IBaseBeforeDeleteEntityLogic> baseBeforeDeleteEntityLogics, List<BaseAfterDeleteEntityLogic> baseAfterDeleteEntityLogics, IIdentityManager identityManager, IRestrictedItemAccessManager accessManager, IDataService<DomainClasses.Entities.EntityScheme, Guid> dataSrv, IEntitySchemeFacade schemeFacade, IEntityDataService entityDataService, ICategoryDataService categoryDataService) : base(baseBeforeAddingEntityLogics, baseAfterAddingEntityLogics, baseAfterUpdateEntityLogics, baseBeforeUpdateEntityLogics, baseBeforeDeleteEntityLogics, baseAfterDeleteEntityLogics, identityManager, accessManager, dataSrv)
        {
            _schemeFacade = schemeFacade;
            _entityDataService = entityDataService;
            _categoryDataService = categoryDataService;
            _dataService = dataSrv as IEntitySchemeDataService;
        }

        public EntitySchemeListResponse GetAsync(EntitySchemeListRequest request)
        {
            var query = GetQueryByRequestOwnerAccess(request, _dataService.Query);
            var allowedItems = query.Where(x => x.Type == request.Query.Type).Paginate(request.Query).ToList().MapToViewModel();
            return new EntitySchemeListResponse()
            {
                Access = ResponseAccess.Granted,
                Count = query.Count(),
                EntitySchemes = allowedItems
            };
        }

        private IQueryable<DomainClasses.Entities.EntityScheme> GetQueryByRequestOwnerAccess(EntitySchemeListRequest request, IQueryable<DomainClasses.Entities.EntityScheme> dataServiceQuery)
        {
            return AccessManager.Filter(dataServiceQuery
                .Include(x => x.FieldDefenitions)
                .Include(x => x.FieldDefenitionGroups)
                .Include(x => x.FieldDefenitionGroups.Select(fdg=>fdg.FieldDefenitions))
                .OrderByDescending(x => x.Id).ToList().AsQueryable());
        }


        public async Task<EntitySchemeResponse> GetByIdAsync(EntitySchemeGetRequest request)
        {
            var scheme = await _dataService.GetAsync(request.Id);
            if (!HasAccessToSee(scheme, request.RequestOwner)) return UnauthorizedRequest();

            return new EntitySchemeResponse()
            {
                EntityScheme = scheme.MapToViewModel(),
                Access = ResponseAccess.Granted,
            };
        }

        public async Task<EntitySchemeResponse> AddAsync(EntitySchemeAddRequest request)
        {
            if (!request.RequestOwner.IsInRole(PreDefRoles.Developer))
                return UnauthorizedRequest();

            await _schemeFacade.AddNewSchemeToSystemAsync(request.ViewModel);
            return Success();
        }

        public async Task<EntitySchemeResponse> UpdateAsync(EntitySchemeUpdateRequest request)
        {
            if (!request.RequestOwner.IsInRole(PreDefRoles.Developer)) return UnauthorizedRequest();
            var scheme = await _dataService.GetAsync(request.ViewModel.Id);
            if (!HasAccessToManage(scheme, request.RequestOwner)) return UnauthorizedRequest();
            await _schemeFacade.UpdateSchemeAsync(request.ViewModel);
            return Success();
        }

        public async Task<EntitySchemeResponse> DeleteAsync(EntitySchemeDeleteRequest request)
        {
            if (!request.RequestOwner.IsInRole(PreDefRoles.Developer)) return UnauthorizedRequest();
            var scheme = await _dataService.GetAsync(request.Id);
            if (!HasAccessToManage(scheme, request.RequestOwner)) return UnauthorizedRequest();
            if (_entityDataService.Query.Any(x => x.SchemeId == scheme.Id)) { return new EntitySchemeResponse() { Access = ResponseAccess.BadRequest, Message = "موجودیت هایی با این  قالب وجود دارند!" }; }
            if (_categoryDataService.Query.Any(x => x.SchemeId == scheme.Id)) { return new EntitySchemeResponse() { Access = ResponseAccess.BadRequest, Message = "دسته بندی هایی با این  قالب وجود دارند!" }; }

            await _dataService.DeleteAsync(request.Id);
            return Success();
        }

        public async Task<EntitySchemeResponse> GetByHandleAsync(EntitySchemeGetRequest request)
        {
            var scheme = await _dataService.Query.Include(x => x.FieldDefenitions).Include(x => x.FieldDefenitionGroups).Include(x => x.FieldDefenitionGroups.Select(fg=>fg.FieldDefenitions)).FirstOrDefaultAsync(x => x.Handle == request.Handle);
            if (!HasAccessToSee(scheme, request.RequestOwner)) return UnauthorizedRequest();

            return new EntitySchemeResponse()
            {
                EntityScheme = scheme.MapToViewModel(),
                Access = ResponseAccess.Granted,
            };
        }
    }
}
