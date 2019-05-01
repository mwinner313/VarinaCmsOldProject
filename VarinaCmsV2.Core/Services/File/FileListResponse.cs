using System.Collections.Generic;
using VarinaCmsV2.ViewModel.FileData;

namespace VarinaCmsV2.Core.Services.File
{
    public class FileListResponse:IServiceResponse
    {
        public ResponseAccess Access { get; set; }
        public string Message { get; set; }
        public List<GroupedByDateFile> FilesData { get; set; }
    }
}