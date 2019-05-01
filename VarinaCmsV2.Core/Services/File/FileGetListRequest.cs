using System.Security.Principal;

namespace VarinaCmsV2.Core.Services.File
{
    public class FileGetListRequest:IServiceRequest
    {
        public IPrincipal RequestOwner { get; set; }
        public FileQuery Query { get; set; }
    }
}