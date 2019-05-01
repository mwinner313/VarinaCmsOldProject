using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.Common;
using VarinaCmsV2.Core.Services;
using VarinaCmsV2.Core.Services.File;
using VarinaCmsV2.Data.Contracts;

namespace VarinaCmsV2.Core.ServiceObservers
{
    public class FileServiceSubject : IFileService
    {
        private IFileService _fileService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly List<FileDataObserver> _observers = new List<FileDataObserver>();
        public FileServiceSubject(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddObserver(FileDataObserver observer)
        {
            _observers.Add(observer);
        }

        public void SetWrapee(IFileService fileService)
        {
            if(_fileService != null)throw new InvalidOperationException("fileService is set before !");
            _fileService = fileService;
        }

        public async Task<FileListResponse> Get(FileGetListRequest request)
        {
            return await _fileService.Get(request);
        }

        public async Task<FileResponse> Get(FileGetRequest request)
        {
            return await _fileService.Get(request);
        }

        public async Task<FileResponse> Add(FileAddRequest request)
        {
            var res = await _fileService.Add(request);
            if (res.Access == ResponseAccess.Granted)
            {
                 _observers.ForEach(x => x.FileAdded(res, request));
                await _unitOfWork.SaveChangesAsync();
            }

            return res;
        }

        public async Task<FileResponse> Edit(FileEditRequest request)
        {
            var res = await _fileService.Edit(request);
            if (res.Access == ResponseAccess.Granted)
            {
                _observers.ForEach(x => x.FileEdited(res, request));
                await _unitOfWork.SaveChangesAsync();
            }
            return res;
        }

        public async Task<FileResponse> Delete(FileDeleteRequest request)
        {
            var res = await _fileService.Delete(request);
            if (res.Access == ResponseAccess.Granted)
            {
                 _observers.ForEach(x => x.FileDeleted(res, request));
                await _unitOfWork.SaveChangesAsync();
            }
            return res;
        }
    }
}
