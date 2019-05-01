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
using VarinaCmsV2.Core.Services;
using VarinaCmsV2.Core.Services.FormScheme;
using VarinaCmsV2.Data.Contracts;
using VarinaCmsV2.DomainClasses.Entities;
using VarinaCmsV2.Security.Contracts;
using VarinaCmsV2.ViewModel.Entities;

namespace VarinaCmsV2.Core.Logic.Services.FormScheme
{
    public class FormSchemeService : BaseService<DomainClasses.Entities.FormScheme, FormSchemeResponse, FormSchemeListReponse>,
        IFormSchemeService
    {
        public async Task<FormSchemeListReponse> Get(FormSchemeGetListRequest request)
        {
            var query =
                string.IsNullOrWhiteSpace(request.Query.SearchText)
                    ? _formSchemeDataSrv.Query
                    : _formSchemeDataSrv.Query.Where(x => x.Title.Contains(request.Query.SearchText));
            return new FormSchemeListReponse()
            {
                Models = await query.OrderByDescending(x => x.Id).Paginate(request.Query).ToViewModelListAsync(),
                Count = query.LongCount(),
                Access = ResponseAccess.Granted
            };
        }

        public async Task<FormSchemeResponse> Get(FormSchemeGetRequest request)
        {
            var formScheme = await
                _formSchemeDataSrv.Query.Include(x => x.FieldDefenitions)
                    .FirstAsync(x => x.Handle == request.FormSchemeHandle);
            formScheme.FieldDefenitions = formScheme.FieldDefenitions.OrderBy(x => x.Order).ToList();
            return new FormSchemeResponse()
            {
                Access = ResponseAccess.Granted,
                Model = formScheme.MapToViewModel()
            };
        }

        public async Task<FormSchemeResponse> Add(FormSchemeAddReqest request)
        {
            var model = request.Model.MapToModel();
            await _formSchemeDataSrv.AddAsync(model);
            return new FormSchemeResponse() { Access = ResponseAccess.Granted, Model = model.MapToViewModel() };
        }

        public async Task<FormSchemeResponse> Edit(FormSchemeEditRequest request)
        {
            var updating =
                _formSchemeDataSrv.Query.First(x => x.Id == request.FormSchemeId);
            request.Model.MapToModel(updating);
            updating.FieldDefenitions.Clear();
            HandleFieldDefenitions(updating.Id, request.Model.FieldDefenitions);
            await _formSchemeDataSrv.UpdateAsync(updating);

            return new FormSchemeResponse() { Access = ResponseAccess.Granted, Model = updating.MapToViewModel() };
        }

        private void HandleFieldDefenitions(Guid formSchemeId, List<FieldDefenitionAddOrUpdateModel> reciviedFds)
        {
            DeleteNonAnyMoreExistingFdsFrom(reciviedFds, formSchemeId);
            UpdateFds(reciviedFds.Where(x => x.Id != Guid.Empty).ToList(), formSchemeId);
            AddFds(reciviedFds.Where(x => x.Id == Guid.Empty).ToList(), formSchemeId);
        }

        private void AddFds(List<FieldDefenitionAddOrUpdateModel> newFdsVm, Guid formSchemeId)
        {
            var newFds = newFdsVm.MapToModel();
            newFds.ForEach(x => { x.FormSchemeId = formSchemeId; });
            newFds.ForEach(_unitOfWork.Add);
        }

        private void UpdateFds(List<FieldDefenitionAddOrUpdateModel> updatingsVm, Guid formSchemeId)
        {
            updatingsVm.ForEach(x =>
            {
                var updating = _fieldDefenitionDataService.Query.Single(f => f.Id == x.Id);
                x.MapToModel(updating);
                _unitOfWork.Update(updating);
            });
        }

        private void DeleteNonAnyMoreExistingFdsFrom(List<FieldDefenitionAddOrUpdateModel> reciviedFds,
            Guid formSchemeId)
        {
            var reciviedFdIds = reciviedFds.Select(x => x.Id);
            _fieldDefenitionDataService.Query.Where(x => x.FormSchemeId == formSchemeId && !reciviedFdIds.Contains(x.Id))
                .ToList()
                .ForEach(_unitOfWork.Delete);
        }

        public async Task<FormSchemeResponse> Delete(FormSchemeDeleteRequest request)
        {
            await _formSchemeDataSrv.DeleteAsync(request.FormSchemeId);
            return new FormSchemeResponse() { Access = ResponseAccess.Granted };
        }

        private readonly IUnitOfWork _unitOfWork;
        private readonly IFormSchemeDataService _formSchemeDataSrv;
        private readonly IFieldDefenitionDataService _fieldDefenitionDataService;

        public FormSchemeService(List<IBaseBeforeAddingEntityLogic> baseBeforeAddingEntityLogics,
            List<BaseAfterAddingEntityLogic> baseAfterAddingEntityLogics,
            List<IBaseAfterUpdatingEntityLogic> baseAfterUpdateEntityLogics,
            List<IBaseBeforeUpdatingEntityLogic> baseBeforeUpdateEntityLogics,
            List<IBaseBeforeDeleteEntityLogic> baseBeforeDeleteEntityLogics,
            List<BaseAfterDeleteEntityLogic> baseAfterDeleteEntityLogics, IIdentityManager identityManager,
            IRestrictedItemAccessManager accessManager,
            IFormSchemeDataService dataSrv, IFieldDefenitionDataService fieldDefenitionDataService,
            IUnitOfWork unitOfWork)
            : base(
                baseBeforeAddingEntityLogics, baseAfterAddingEntityLogics, baseAfterUpdateEntityLogics,
                baseBeforeUpdateEntityLogics, baseBeforeDeleteEntityLogics, baseAfterDeleteEntityLogics, identityManager,
                accessManager, dataSrv)
        {
            _formSchemeDataSrv = dataSrv;
            _fieldDefenitionDataService = fieldDefenitionDataService;
            _unitOfWork = unitOfWork;
        }
    }
}