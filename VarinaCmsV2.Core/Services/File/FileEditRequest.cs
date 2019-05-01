using System;
using System.Security.Principal;
using VarinaCmsV2.ViewModel.FileData;

namespace VarinaCmsV2.Core.Services.File
{
    public class FileEditRequest:IServiceRequest
    {
        public IPrincipal RequestOwner { get; set; }
        public FileDataAddOrUpdateVeiwModel ViewModel { get; set; }
        public Guid FileId { get; set; }
    }
}