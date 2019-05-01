using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using PersianDate;
using VarinaCmsV2.Common;
using VarinaCmsV2.Common.WebApi;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.Core.Contracts.Security;
using VarinaCmsV2.Core.Infrastructure;
using VarinaCmsV2.Core.Logic.Helpers;
using VarinaCmsV2.Core.Services;
using VarinaCmsV2.Core.Services.File;
using VarinaCmsV2.Data.Contracts;
using VarinaCmsV2.FileManager.Files;
using VarinaCmsV2.Security.Contracts;
using VarinaCmsV2.ViewModel.FileData;
using VarinaCmsV2.ViewModel.Meta;

namespace VarinaCmsV2.Core.Logic.Services.FileData
{
    public class FileService : BaseService<DomainClasses.Entities.FileData, FileResponse, FileListResponse>,
        IFileService
    {
        private readonly IFileDataDataService _fileDataService;
        private readonly IMetaDataDataService _metaDataService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileManager _fileManager;
        private readonly string _appDir = AppDomain.CurrentDomain.BaseDirectory;
        private readonly string _saveDir = AppDomain.CurrentDomain.BaseDirectory + SettingHelper.UploadsBasePath;
        public FileService(List<IBaseBeforeAddingEntityLogic> baseBeforeAddingEntityLogics,
            List<BaseAfterAddingEntityLogic> baseAfterAddingEntityLogics,
            List<IBaseAfterUpdatingEntityLogic> baseAfterUpdateEntityLogics,
            List<IBaseBeforeUpdatingEntityLogic> baseBeforeUpdateEntityLogics,
            List<IBaseBeforeDeleteEntityLogic> baseBeforeDeleteEntityLogics,
            List<BaseAfterDeleteEntityLogic> baseAfterDeleteEntityLogics, IIdentityManager identityManager,
            IRestrictedItemAccessManager accessManager, IFileDataDataService dataSrv, IMetaDataDataService metaDataService, IFileManager fileManager, IUnitOfWork unitOfWork)
            : base(
                baseBeforeAddingEntityLogics, baseAfterAddingEntityLogics, baseAfterUpdateEntityLogics,
                baseBeforeUpdateEntityLogics, baseBeforeDeleteEntityLogics, baseAfterDeleteEntityLogics, identityManager,
                accessManager, dataSrv)
        {
            _fileDataService = dataSrv;
            _metaDataService = metaDataService;
            _fileManager = fileManager;
            _unitOfWork = unitOfWork;
        }

        public async Task<FileListResponse> Get(FileGetListRequest request)
        {
            var files = _fileDataService.Query.Filter(request.Query).OrderByDescending(x => x.CreateDateTime).AsQueryable();

            return new FileListResponse
            {
                Access = ResponseAccess.Granted,
                FilesData = (await files.Paginate(request.Query).MapListToViewModelWithMeta(_metaDataService.Query))
                    .GroupBy(x => x.CreateDateTime.Date,
                        x => x,
                        (key, group) =>
                            new GroupedByDateFile
                            {
                                DateTime = ConvertDate.ToFa(key, "yyyy/MM/dd"),
                                Files = group.ToList()
                            })
                    .OrderByDescending(x => x.DateTime)
                    .ToList()
            };
        }

        public async Task<FileResponse> Get(FileGetRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<FileResponse> Add(FileAddRequest request)
        {
            var file = await _fileManager.SaveAsync(request.File,_saveDir);
            
            var fileData = new DomainClasses.Entities.FileData()
            {
                Name = file.SavedFileName,
                Path = file.SavedFilePath,
                Size = file.Size,
                Extension = file.Extension
            };

            await base.BaseBeforeAddAsync(fileData, request.RequestOwner);
            await _fileDataService.AddAsync(fileData);
            await base.BaseAfterAddAsync(fileData, request.RequestOwner);
            _unitOfWork.Entry(fileData).Reference(x=>x.Creator).Load();
            return new FileResponse() {Access = ResponseAccess.Granted,Model = fileData.MapToViewModel() };
        }

        public async Task<FileResponse> Edit(FileEditRequest request)
        {
            var editingFile = _fileDataService.Query.First(x => x.Id == request.FileId);
            if (editingFile.Name != request.ViewModel.Name)
                await BeCareFullAboutBothDataAndFileChangesAndDoTheRestEdit(editingFile, request);
            else await DoTheRestEdit(editingFile, request);
            return new FileResponse()
            {
                Access = ResponseAccess.Granted,
                Model = editingFile.MapToViewModelWithMeta(request.ViewModel.MetaDatas.MapToModelList())
            };
        }

        private async Task BeCareFullAboutBothDataAndFileChangesAndDoTheRestEdit(DomainClasses.Entities.FileData editingFile, FileEditRequest request)
        {
            var oldName = editingFile.Name;
            var fileEditRes = _fileManager.Rename(_appDir + editingFile.Path, request.ViewModel.Name, request.ViewModel.ActionPrompt);
            editingFile.Path = fileEditRes.SavedFilePath;
            try
            {
                await DoTheRestEdit(editingFile, request);
            }
            catch (Exception e)
            {
                _fileManager.Rename(_appDir + fileEditRes.SavedFilePath, oldName, request.ViewModel.ActionPrompt);
                throw;
            }
        }


        private async Task DoTheRestEdit(DomainClasses.Entities.FileData editingFile, FileEditRequest request)
        {
            request.ViewModel.MapToExisting(editingFile);
            UpdateMetaDatas(request.ViewModel.MetaDatas.Where(x => x.HasChanges).ToList());
            await base.BaseBeforeUpdateAsync(editingFile, request.RequestOwner);
            await _fileDataService.UpdateAsync(editingFile);
            await base.BaseAfterUpdateAsync(editingFile, request.RequestOwner);
        }

        private void UpdateMetaDatas(List<MetaDataAddOrUpdateViewModel> changedMetas)
        {
            if (changedMetas == null || changedMetas.Count == 0) return;
            var changedIds = changedMetas.Select(c => c.Id);
            var editingMetas = _metaDataService.Query.Where(x => changedIds.Contains(x.Id)).ToList();
            changedMetas.MapToExistingModelList(editingMetas);
            editingMetas.ForEach(_unitOfWork.Update);
        }

        public async Task<FileResponse> Delete(FileDeleteRequest request)
        {
            var file = _fileDataService.Query.First(x => x.Id == request.FileId);
            await base.BaseBeforeDeleteAsync(file, request.RequestOwner);
            _fileManager.Delete(_appDir + file.Path);
            await _fileDataService.DeleteAsync(request.FileId);
            await base.BaseAfterDeleteAsync(file, request.RequestOwner);
            return Success();
        }
    }
}