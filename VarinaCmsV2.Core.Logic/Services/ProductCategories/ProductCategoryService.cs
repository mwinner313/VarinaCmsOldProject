using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.Core.Contracts.Security;
using VarinaCmsV2.Core.Infrastructure;
using VarinaCmsV2.Core.Logic.Helpers;
using VarinaCmsV2.Core.LogicFacade;
using VarinaCmsV2.Core.Services;
using VarinaCmsV2.Core.Services.ProductCategories;
using VarinaCmsV2.Data.Contracts;
using VarinaCmsV2.DomainClasses.Entities.EShop;
using VarinaCmsV2.Security.Contracts;
using VarinaCmsV2.ViewModel.Meta;

namespace VarinaCmsV2.Core.Logic.Services.ProductCategories
{
    public class ProductCategoryService :
        BaseService<ProductCategory, ProductCategoryGetResponse, ProductCategoryListResponse>, IProductCategoryService
    {
        private readonly IProductCategoryDataService _categoryData;
        private readonly IFieldDefentionFacade _fieldDefentionFacade;
        private readonly IMetaDataDataService _metaDataService;
        private readonly IProductDataService _productDataService;
        private readonly IUnitOfWork _unitOfWork;

        public ProductCategoryService(List<IBaseBeforeAddingEntityLogic> baseBeforeAddingEntityLogics,
            List<BaseAfterAddingEntityLogic> baseAfterAddingEntityLogics,
            List<IBaseAfterUpdatingEntityLogic> baseAfterUpdateEntityLogics,
            List<IBaseBeforeUpdatingEntityLogic> baseBeforeUpdateEntityLogics,
            List<IBaseBeforeDeleteEntityLogic> baseBeforeDeleteEntityLogics,
            List<BaseAfterDeleteEntityLogic> baseAfterDeleteEntityLogics, IIdentityManager identityManager,
            IRestrictedItemAccessManager accessManager, IProductCategoryDataService categoryData,
            IUnitOfWork unitOfWork, IFieldDefentionFacade fieldDefentionFacade, IMetaDataDataService metaDataService,
            IProductDataService productDataService) : base(
            baseBeforeAddingEntityLogics, baseAfterAddingEntityLogics, baseAfterUpdateEntityLogics,
            baseBeforeUpdateEntityLogics, baseBeforeDeleteEntityLogics, baseAfterDeleteEntityLogics, identityManager,
            accessManager, categoryData)
        {
            _categoryData = categoryData;
            _unitOfWork = unitOfWork;
            _fieldDefentionFacade = fieldDefentionFacade;
            _metaDataService = metaDataService;
            _productDataService = productDataService;
        }

        public async Task<ProductCategoryListResponse> Get(ProductCategoryListRequest request)
        {
            var categories = AccessManager.Filter(_categoryData.Query.Include(x => x.FieldDefenitions)
                .Include(x => x.FieldDefenitionGroups)
                .Include(x => x.FieldDefenitionGroups.Select(fdg => fdg.FieldDefenitions)).Include(x => x.Childs)
                .FilterByQuery(request.Query));

            return new ProductCategoryListResponse
            {
                Access = ResponseAccess.Granted,
                Categories = await categories.ToViewModelListIncludeMetaDataAsync(_metaDataService),
                Count = await categories.CountAsync()
            };
        }

        public async Task<ProductCategoryAddResponse> Add(ProductCategoryAddRequest request)
        {
            var categoryToAdd = request.Model.MapToModel();
            categoryToAdd.FieldDefenitions?.Clear();
            categoryToAdd.FieldDefenitionGroups?.Clear();
            await BaseBeforeAddAsync(categoryToAdd, request.RequestOwner);
            await _categoryData.AddAsync(categoryToAdd);
            request.Model.FieldDefenitions.ToList().MapToModel().ForEach(x =>
            {
                x.ProductCategoryId = categoryToAdd.Id;
                _fieldDefentionFacade.PendToAdd(x);
            });
            request.Model.FieldDefenitionGroups.ToList().MapToModel().ForEach(x =>
            {
                x.ProductCategoryId = categoryToAdd.Id;
                _unitOfWork.Add(x);
            });
            await _fieldDefentionFacade.SaveChangesAsync();
            await BaseAfterAddAsync(categoryToAdd, request.RequestOwner);
            HandleMetaData(request.Model.MetaData, categoryToAdd);
            await _unitOfWork.SaveChangesAsync();
            return new ProductCategoryAddResponse
            {
                Access = ResponseAccess.Granted,
                Category = categoryToAdd.MapToViewModel()
            };
        }

        public async Task<ProductCategoryEditResponse> Edit(ProductCategoryEditRequest request)
        {
            var categoryToEdit = await _categoryData.Query.Include(x => x.Scheme)
                .FirstAsync(x => x.Id == request.Id);
            if (!HasAccessToManage(categoryToEdit, request.RequestOwner)) return new ProductCategoryEditResponse();
            request.Model.FieldDefenitionGroups?.Clear();
            request.Model.FieldDefenitions?.Clear();
            request.Model.MapToExisting(categoryToEdit);
            HandleMetaData(request.Model.MetaData, categoryToEdit);

            await BaseBeforeUpdateAsync(categoryToEdit, request.RequestOwner);
            await _categoryData.UpdateAsync(categoryToEdit);
          
            await BaseAfterUpdateAsync(categoryToEdit, request.RequestOwner);
            return new ProductCategoryEditResponse
            {
                Access = ResponseAccess.Granted,
                Category = categoryToEdit.MapToViewModel()
            };
        }

        public async Task<ProductCategoryDeleteResponse> Delete(ProductCategoryDeleteRequest request)
        {
            if (_productDataService.Query.Any(x => x.PrimaryCategoryId == request.Id))
                return new ProductCategoryDeleteResponse
                {
                    Access = ResponseAccess.BadRequest,
                    Message = "محصولاتی با این دسته بندی وجود دارند"
                };

            var categoryToDelete = await _categoryData.GetAsync(request.Id);
            if (!HasAccessToManage(categoryToDelete, request.RequestOwner)) return new ProductCategoryDeleteResponse();


            await BaseBeforeDeleteAsync(categoryToDelete, request.RequestOwner);
            await _categoryData.DeleteAsync(request.Id);
            await BaseAfterDeleteAsync(categoryToDelete, request.RequestOwner);
            return new ProductCategoryDeleteResponse { Access = ResponseAccess.Granted };
        }

        public async Task<SimpleResonse> UpdateParentListRequest(ParentChildUpdateRequest request)
        {
            if (request.ViewModel.Any(x => x.Id == x.ParentId))
                return new SimpleResonse { Access = ResponseAccess.BadRequest };
            var ids = request.ViewModel.Select(x => x.Id).ToList();
            var categories = _categoryData.Query.Where(x => ids.Contains(x.Id)).ToList();
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
            return new SimpleResonse { Access = ResponseAccess.Granted };
        }

        private void HandleMetaData(List<MetaDataAddOrUpdateViewModel> metaDatas, ProductCategory cat)
        {
            DeleteNonExistingAnyMoreMetaData(metaDatas, cat);
            UpdateMetaDatas(metaDatas.Where(x => x.HasChanges).ToList());
            foreach (var meta in metaDatas.Where(x => x.Id == Guid.Empty).MapToModelList())
            {
                meta.TargetType = ProductCategory.MetaTypeName;
                meta.TargetId = cat.Id;
                _unitOfWork.Add(meta);
            }
        }

        private void DeleteNonExistingAnyMoreMetaData(List<MetaDataAddOrUpdateViewModel> metaDatas, ProductCategory cat)
        {
            var availibleIds = metaDatas.Where(x => x.Id != Guid.Empty).Select(x => x.Id);
            _metaDataService.Query.Where(x => x.TargetType == ProductCategory.MetaTypeName && x.TargetId == cat.Id &&
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

    }
}