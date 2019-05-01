using System.Net.Http;
using System.Security.Principal;
using System.Web;
using VarinaCmsV2.Common.WebApi;
using VarinaCmsV2.ViewModel.FileData;

namespace VarinaCmsV2.Core.Services.File
{
    public class FileAddRequest:IServiceRequest
    {
        public IPrincipal RequestOwner { get; set; }
        public FileDataAddOrUpdateVeiwModel ViewModel { get; set; }
        public HttpContent File { get; set; } 
    }
}