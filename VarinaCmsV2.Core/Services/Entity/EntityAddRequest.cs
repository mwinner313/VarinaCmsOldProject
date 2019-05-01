using System.Security.Principal;
using VarinaCmsV2.ViewModel.Entities;

namespace VarinaCmsV2.Core.Services.Entity
{
    public class EntityAddRequest
    {
        public IPrincipal RequestOwner { get; set; }
        public EntityAddOrUpdateViewModel ViewModel { get; set; }
    }
}