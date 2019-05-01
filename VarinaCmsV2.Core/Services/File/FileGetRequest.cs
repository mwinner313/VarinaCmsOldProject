using System;
using System.Security.Principal;

namespace VarinaCmsV2.Core.Services.File
{
    public class FileGetRequest:IServiceRequest
    {
        public IPrincipal RequestOwner { get; set; }
        public Guid FileId { get; set; }
    }
}