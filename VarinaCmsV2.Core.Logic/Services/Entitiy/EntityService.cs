using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.Common;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.Core.Contracts.Security;
using VarinaCmsV2.Core.Infrastructure;
using VarinaCmsV2.Core.Logic.Helpers;
using VarinaCmsV2.Core.LogicFacade;
using VarinaCmsV2.Core.Security.Premissions;
using VarinaCmsV2.Core.Services;
using VarinaCmsV2.Core.Services.Entity;
using VarinaCmsV2.Core.Services.Page;
using VarinaCmsV2.Data.Contracts;
using VarinaCmsV2.DomainClasses.Entities;
using VarinaCmsV2.Security.Contracts;
using VarinaCmsV2.ViewModel.Entities;

namespace VarinaCmsV2.Core.Logic.Services.Entitiy
{
    public class EntityService : BaseService<Entity, EntityResponse, EntityListResponse>, IEntityService
    {
        private readonly IEntityDataService _dataSrv;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISecurityLogger _securityLogger;
        private readonly IEntityFacade _entityFacade;

        public EntityService(List<IBaseBeforeAddingEntityLogic> baseBeforeAddingEntityLogics,
            List<BaseAfterAddingEntityLogic> baseAfterAddingEntityLogics,
            List<IBaseAfterUpdatingEntityLogic> baseAfterUpdateEntityLogics,
            List<IBaseBeforeUpdatingEntityLogic> baseBeforeUpdateEntityLogics,
            List<IBaseBeforeDeleteEntityLogic> baseBeforeDeleteEntityLogics,
            List<BaseAfterDeleteEntityLogic> baseAfterDeleteEntityLogics, IIdentityManager identityManager,
            IRestrictedItemAccessManager accessManager, IEntityDataService dataSrv, ISecurityLogger securityLogger,
            IEntityFacade entityFacade, IUnitOfWork unitOfWork)
            : base(
                baseBeforeAddingEntityLogics, baseAfterAddingEntityLogics, baseAfterUpdateEntityLogics,
                baseBeforeUpdateEntityLogics, baseBeforeDeleteEntityLogics, baseAfterDeleteEntityLogics, identityManager,
                accessManager, dataSrv)
        {
            _dataSrv = dataSrv;
            _securityLogger = securityLogger;
            _entityFacade = entityFacade;
            _unitOfWork = unitOfWork;
        }

        public async Task<EntityResponse> Get(EntityGetRequest request)
        {
            if (!HasPremission(request.RequestOwner, EntityPremission.CanSee))
                return UnauthorizedRequest();

            var entity = await _dataSrv.GetAsync(request.Id);

            if (!HasAccessToSee(entity, request.RequestOwner)) return UnauthorizedRequest();

            return new EntityResponse() { Entity = entity.MapToViewModel(), Access = ResponseAccess.Granted };
        }

        public async Task<EntityListResponse> Get(EntityListRequest request)
        {
            if (!HasPremission(request.RequestOwner, EntityPremission.CanSee)) return UnauthorizedListRequest();
            var entities = AccessManager.Filter(_dataSrv.Query).FilterByEntityQuery(request.Query).ApplyEntityOrderBy(request.Query.Order);
            return new EntityListResponse()
            {
                Access = ResponseAccess.Granted,
                Entities =
                    await entities.Include(x => x.Scheme).Include(x => x.PrimaryCategory).OrderByDescending(x => x.CreateDateTime)
                        .Paginate(request.Query ?? new Pagenation())
                        .ToViewModelListAsync(),
                Count = entities.Count()
            };
        }

        public async Task<EntityResponse> Add(EntityAddRequest request)
        {
            if (!HasPremission(request.RequestOwner, EntityPremission.CanAdd))
            {
                _securityLogger.LogDangeriousAddAttemp(request.RequestOwner, request.ViewModel);
                return UnauthorizedRequest();
            }
            var model = request.ViewModel.MapToEntity();
            model.Fields = request.ViewModel.Fields.MapToModel();
            await BaseBeforeAddAsync(model, request.RequestOwner);
            await _entityFacade.AddAsync(model);
            await BaseAfterAddAsync(model, request.RequestOwner);
            var res = Success();

            return res;
        }

        public async Task<EntityResponse> Update(EntityUpdateRequest request)
        {
            if (!HasPremission(request.RequestOwner, EntityPremission.CanEdit))
            {
                _securityLogger.LogDangeriousUpdateAttemp(request.RequestOwner, request.ViewModel);
                return UnauthorizedRequest();
            }

            var updatingEntity = _dataSrv.Query
                .Include(x => x.Scheme)
                .Include(x => x.Scheme.FieldDefenitions)
                .Include(x => x.Scheme.FieldDefenitionGroups)
                .Include(x => x.Scheme.FieldDefenitionGroups.Select(fdg => fdg.FieldDefenitions))
                .Include(x => x.PrimaryCategory)
                .Include(x => x.PrimaryCategory.FieldDefenitions)
                .Include(x => x.RelatedCategories)
                .Include(x => x.Tags)
                .Include(x => x.Fields)
                .First(x => x.Id == request.EntityId);

            if (!HasAccessToManage(updatingEntity, request.RequestOwner))
            {
                _securityLogger.LogDangeriousUpdateAttemp(request.RequestOwner, request.ViewModel);
                return UnauthorizedRequest();
            }

            request.ViewModel.MapToExisting(updatingEntity);

            await Update(request, updatingEntity);

            return Success();
        }

        private async Task Update(EntityUpdateRequest request, Entity updatingEntity)
        {
            HandleFields(updatingEntity.Fields, request.ViewModel.Fields);
            await BaseBeforeUpdateAsync(updatingEntity, request.RequestOwner);
            await _entityFacade.UpdateAsync(updatingEntity);
            await BaseAfterUpdateAsync(updatingEntity, request.RequestOwner);
        }

        private void HandleFields(ICollection<Field> olds, List<FieldAddOrUpdateViewModel> newOnes)
        {
            var newOneIds = newOnes.Select(x => x.Id);
            olds.Where(x => !newOneIds.Contains(x.Id)).ToList().ForEach(_unitOfWork.Delete);
            newOnes.Where(x => x.Id == Guid.Empty).ToList().MapToModel().ForEach(_unitOfWork.Add);
            newOnes.Where(x => x.Id != Guid.Empty).ToList().ForEach(x =>
                {
                    var old = olds.First(o => o.Id == x.Id);
                    x.MapToExisting(old);
                    _unitOfWork.Update(old);
                });
        }


        public async Task<EntityResponse> Delete(EntityDeleteRequest request)
        {
            var entity = await _dataSrv.GetAsync(request.EntityId);
            if (!HasPremission(request.RequestOwner, EntityPremission.CanDelete) ||
                !HasAccessToManage(entity, request.RequestOwner))
            {
                _securityLogger.LogDangeriousDeleteAttemp(request.RequestOwner, entity);
                return UnauthorizedRequest();
            }
            await BaseBeforeDeleteAsync(entity, request.RequestOwner);
            await _entityFacade.DeleteAsync(request.EntityId);
            await BaseAfterDeleteAsync(entity, request.RequestOwner);
            return Success();
        }
    }
}