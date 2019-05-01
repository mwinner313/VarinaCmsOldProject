using System;
using System.Threading.Tasks;
using VarinaCmsV2.Core.Services.File;
using VarinaCmsV2.ViewModel.FileData;

namespace VarinaCmsV2.Core.ServiceObservers
{
    public abstract class FileDataObserver
    {
        protected readonly string AppDir = AppDomain.CurrentDomain.BaseDirectory;
        public abstract void FileAdded(FileResponse res, FileAddRequest req);
        public abstract void FileDeleted(FileResponse res, FileDeleteRequest req);
        public abstract void FileEdited(FileResponse res, FileEditRequest req);
    }
}