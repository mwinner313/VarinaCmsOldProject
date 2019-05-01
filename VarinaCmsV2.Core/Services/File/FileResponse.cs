using System.Security.Principal;
using VarinaCmsV2.ViewModel.FileData;

namespace VarinaCmsV2.Core.Services.File
{
    public class FileResponse: IServiceResponse
    {
        public ResponseAccess Access { get; set; }
        public string Message { get; set; }
        public FileDataViewModelWithMeta Model { get; set; }
    }
}