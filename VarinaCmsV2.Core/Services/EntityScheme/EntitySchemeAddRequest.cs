using System.Security.Principal;
using VarinaCmsV2.ViewModel.Entities;

namespace VarinaCmsV2.Core.Services.EntityScheme
{
    public class EntitySchemeAddRequest
    {
        public IPrincipal RequestOwner { get; set; }
        public EntitySchemeViewModel ViewModel { get; set; }
    }
}