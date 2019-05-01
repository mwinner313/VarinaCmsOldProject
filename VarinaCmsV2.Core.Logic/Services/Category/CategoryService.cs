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
using VarinaCmsV2.Core.Services.Category;
using VarinaCmsV2.Data.Contracts;
using VarinaCmsV2.Security.Contracts;
using VarinaCmsV2.ViewModel.Meta;
using Z.EntityFramework.Plus;

namespace VarinaCmsV2.Core.Logic.Services.Category
{
    public class CategoryService : BaseService<DomainClasses.Entities.Category, CategoryResponse, CategoryListResponse>, ICategoryService
    {
        private readonly ICategoryDataService _dataSrv;
        private readonly IFieldDefentionFacade _fieldDefentionFacade;
        private readonly IMetaDataDataService _metaDataService;
        private readonly IEntityDataService _entityDataService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUrlBuilder _urlBuilder;
        public CategoryService(List<IBaseBeforeAddingEntityLogic> baseBeforeAddingEntityLogics, List<BaseAfterAddingEntityLogic> baseAfterAddingEntityLogics, List<IBaseAfterUpdatingEntityLogic> baseAfterUpdateEntityLogics, List<IBaseBeforeUpdatingEntityLogic> baseBeforeUpdateEntityLogics, List<IBaseBeforeDeleteEntityLogic> baseBeforeDeleteEntityLogics, List<BaseAfterDeleteEntityLogic> baseAfterDeleteEntityLogics, IIdentityManager identityManager, IRestrictedItemAccessManager accessManager, ICategoryDataService dataSrv, IUnitOfWork unitOfWork, IUrlBuilder urlBuilder, IFieldDefentionFacade fieldDefentionFacade, IMetaDataDataService metaDataService, IEntityDataService entityDataService) : base(baseBeforeAddingEntityLogics, baseAfterAddingEntityLogics, baseAfterUpdateEntityLogics, baseBeforeUpdateEntityLogics, baseBeforeDeleteEntityLogics, baseAfterDeleteEntityLogics, identityManager, accessManager, dataSrv)
        {
            _dataSrv = dataSrv;
            _unitOfWork = unitOfWork;
            _urlBuilder = urlBuilder;
            _fieldDefentionFacade = fieldDefentionFacade;
            _metaDataService = metaDataService;
            _entityDataService = entityDataService;
        }

        public async Task<CategoryListResponse> Get(CategoryListRequest request)
        {
            if (!HasPremission(request.RequestOwner, CategoryPremission.CanSee)) return UnauthorizedListRequest();
            var query = AccessManager.Filter(_dataSrv.Query).FilterByQuery(request.Query, _unitOfWork);

            return new CategoryListResponse()
            {
                Access = ResponseAccess.Granted,
                Categories = await query.Include(x => x.Scheme)
                .Include(x => x.Parent)
                .Include(x => x.FieldDefenitions)
                .Include(x => x.FieldDefenitionGroups)
                .Include(x => x.FieldDefenitionGroups.Select(fdg=>fdg.FieldDefenitions))
                .ToViewModelListIncludeMetaDataAsync(_metaDataService),
            };
        }


        public async Task<CategoryResponse> Add(CategoryAddRequest request)
        {
            var categoryToAdd = request.Model.MapToModel();
            categoryToAdd.FieldDefenitions?.Clear();
            categoryToAdd.FieldDefenitionGroups?.Clear();
            await BaseBeforeAddAsync(categoryToAdd, request.RequestOwner);

            await _dataSrv.AddAsync(categoryToAdd);
            request.Model.FieldDefenitions.ToList().MapToModel().ForEach(x =>
            {
                x.CategoryId = categoryToAdd.Id;
                _fieldDefentionFacade.PendToAdd(x);
            });
            request.Model.FieldDefenitionGroups.ToList().MapToModel().ForEach(x =>
            {
                x.CategoryId = categoryToAdd.Id;
                _unitOfWork.Add(x);
            });
            await _fieldDefentionFacade.SaveChangesAsync();
            await BaseAfterAddAsync(categoryToAdd, request.RequestOwner);
            HandleMetaData(request.Model.MetaData, categoryToAdd);
            await _unitOfWork.SaveChangesAsync();
            return new CategoryResponse()
            {
                Access = ResponseAccess.Granted,
                Category = categoryToAdd.MapToViewModel()
            };
        }

        public async Task<CategoryResponse> Update(CategoryUpdateRequest request)
        {
            var categoryToEdit = await _dataSrv.Query.Include(x => x.FieldDefenitions).Include(x => x.Scheme).FirstAsync(x => x.Id == request.Id);
            if (!HasAccessToManage(categoryToEdit, request.RequestOwner)) return UnauthorizedRequest();
            request.ViewModel.MapToExisting(categoryToEdit);
            HandleMetaData(request.ViewModel.MetaData, categoryToEdit);

            await BaseBeforeUpdateAsync(categoryToEdit, request.RequestOwner);
            await _dataSrv.UpdateAsync(categoryToEdit);
            await HandleFiledDefenitons(request, categoryToEdit);
            await BaseAfterUpdateAsync(categoryToEdit, request.RequestOwner);
            return new CategoryResponse()
            {
                Access = ResponseAccess.Granted,
                Category = categoryToEdit.MapToViewModel()
            };
        }

        private void HandleMetaData(List<MetaDataAddOrUpdateViewModel> metaDatas, DomainClasses.Entities.Category cat)
        {
            DeleteNonExistingAnyMoreMetaData(metaDatas, cat);
            UpdateMetaDatas(metaDatas.Where(x => x.HasChanges).ToList());
            foreach (var meta in metaDatas.Where(x => x.Id == Guid.Empty).MapToModelList())
            {
                meta.TargetType = DomainClasses.Entities.Category.MetaTypeName;
                meta.TargetId = cat.Id;
                _unitOfWork.Add(meta);
            }
        }

        private void DeleteNonExistingAnyMoreMetaData(List<MetaDataAddOrUpdateViewModel> metaDatas, DomainClasses.Entities.Category cat)
        {
            var availibleIds = metaDatas.Where(x => x.Id != Guid.Empty).Select(x => x.Id);
            _metaDataService.Query.Where(x => x.TargetType == DomainClasses.Entities.Category.MetaTypeName && x.TargetId == cat.Id &&
                                              !availibleIds.Contains(x.Id)).ToList().ForEach(_unitOfWork.Delete);
        }


        private void UpdateMetaDatas(List<MetaDataAddOrUpdateViewModel> changedMetas)
        {
            if (changedMetas == null || changedMetas.Count == 0) return;
            var changedIds = changedMetas.Select(c => c.Id);
            var editingMetas = _metaDataService.Query.Where(x => changedIds.Contains(x.Id)).ToList();
            changedMetas.MapToExistingModelList(editingMetas);
            editingMetas.ForEach(_unitOfWork.Update);
        }
        private async Task HandleFiledDefenitons(CategoryUpdateRequest request, DomainClasses.Entities.Category categoryToEdit)
        {
            var fds = request.ViewModel.FieldDefenitions.MapToModel();

            fds.Where(x => x.Id != Guid.Empty).ToList().ForEach(_fieldDefentionFacade.PendToUpdate);
            fds.Where(x => x.Id == Guid.Empty).ToList().ForEach(x =>
              {
                  x.CategoryId = categoryToEdit.Id;
                  _fieldDefentionFacade.PendToAdd(x);
              });
            categoryToEdit.FieldDefenitions.Where(x => !request.ViewModel.FieldDefenitions.Select(f => f.Id).Contains(x.Id))
                .ToList()
                .ForEach(x => _fieldDefentionFacade.PendToDelete(x.Id));
            await _fieldDefentionFacade.SaveChangesAsync();
        }

        public async Task<CategoryResponse> Delete(CategoryDeleteRequest request)
        {
            if (_entityDataService.Query.Any(x => x.PrimaryCategoryId == request.Id))
                return new CategoryResponse() { Access = ResponseAccess.BadRequest, Message = "موجودیتی با این دسته بندی وجود دارند" };

            if (!HasPremission(request.RequestOwner, CategoryPremission.CanDelete)) return UnauthorizedRequest();
            var categoryToDelete = await _dataSrv.GetAsync(request.Id);
            if (!HasAccessToManage(categoryToDelete, request.RequestOwner)) return UnauthorizedRequest();
            await BaseBeforeDeleteAsync(categoryToDelete, request.RequestOwner);
            await _dataSrv.DeleteAsync(request.Id);
            await BaseAfterDeleteAsync(categoryToDelete, request.RequestOwner);
            return Success();
        }

        public async Task<SimpleResonse> UpdateParentListRequest(CategoryParentChildUpdateRequest request)
        {
            if (!HasPremission(request.RequestOwner, CategoryPremission.CanEdit)) return new SimpleResonse() { Access = ResponseAccess.Deny };
            if (request.ViewModel.Any(x => x.Id == x.ParentId)) return new SimpleResonse() { Access = ResponseAccess.BadRequest };
            var ids = request.ViewModel.Select(x => x.Id).ToList();
            var categories = _dataSrv.Query.Where(x => ids.Contains(x.Id) && x.SchemeId == request.SchemeId).ToList();
            foreach (var item in request.ViewModel)
            {
                var cat = categories.First(x => x.Id == item.Id);
                cat.ParentId = item.ParentId;
                cat.Order = item.Order;
                await BaseBeforeUpdateAsync(cat, request.RequestOwner);
                _unitOfWork.Update(cat);
                await BaseAfterUpdateAsync(cat, request.RequestOwner);
            }

            await _unitOfWork.SaveChangesAsync();
            return new SimpleResonse() { Access = ResponseAccess.Granted };
        }
    }
}
