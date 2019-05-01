using System.Security.Principal;

namespace VarinaCmsV2.Core.Services.Page
{
    public class PageListRequest:IServiceRequest
    {
       
        public PageQuery Query { get; set; }
        public IPrincipal RequestOwner { get; set; }
    }
}