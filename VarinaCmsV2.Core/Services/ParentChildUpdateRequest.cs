using System.Collections.Generic;
using System.Security.Principal;
using VarinaCmsV2.ViewModel;

namespace VarinaCmsV2.Core.Services
{
    public class ParentChildUpdateRequest:IServiceRequest
    {
        public List<ParentChildIdViewModel> ViewModel { get; set; }
        public IPrincipal RequestOwner { get; set; }
    }
}