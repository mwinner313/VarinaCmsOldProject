using System.Security.Principal;

namespace VarinaCmsV2.Core.Services.FormScheme
{
    public class FormSchemeGetListRequest:IServiceRequest
    {
        public IPrincipal RequestOwner { get; set; }
        public FormSchemeQuery Query { get; set; }
    }
}