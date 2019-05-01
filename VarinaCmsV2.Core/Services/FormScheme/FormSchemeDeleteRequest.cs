using System;
using System.Security.Principal;

namespace VarinaCmsV2.Core.Services.FormScheme
{
    public class FormSchemeDeleteRequest:IServiceRequest
    {
        public Guid FormSchemeId { get; set; }
        public IPrincipal RequestOwner { get; set; }
    }
}