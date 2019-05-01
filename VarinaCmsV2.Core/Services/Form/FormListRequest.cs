using System.Security.Principal;

namespace VarinaCmsV2.Core.Services.Form
{
    public class FormListRequest:IServiceRequest
    {
        public IPrincipal RequestOwner { get; set; }
        public FormQuery Query { get; set; }
    }
}