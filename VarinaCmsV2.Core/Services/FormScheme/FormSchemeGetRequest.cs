using System;
using System.Security.Principal;

namespace VarinaCmsV2.Core.Services.FormScheme
{
    public class FormSchemeGetRequest:IServiceRequest
    {
        public IPrincipal RequestOwner { get; set; }
        public string FormSchemeHandle { get; set; }
    }
}