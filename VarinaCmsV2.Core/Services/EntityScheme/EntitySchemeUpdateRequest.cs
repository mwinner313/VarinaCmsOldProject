using System.Security.Principal;
using VarinaCmsV2.ViewModel.Entities;

namespace VarinaCmsV2.Core.Services.EntityScheme
{
    public class EntitySchemeUpdateRequest
    {
        public EntitySchemeUpdateViewModel ViewModel { get; set; }
        public IPrincipal RequestOwner { get; set; }
    }
}