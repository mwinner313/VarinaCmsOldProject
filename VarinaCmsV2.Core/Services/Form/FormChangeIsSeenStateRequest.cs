using System;
using System.Security.Principal;

namespace VarinaCmsV2.Core.Services.Form
{
    public class FormChangeIsSeenStateRequest:IServiceRequest
    {
        public IPrincipal RequestOwner { get; set; }
        public Guid FormId { get; set; }
    }
}