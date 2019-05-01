using System;
using System.Security.Principal;
using VarinaCmsV2.ViewModel.Entities;

namespace VarinaCmsV2.Core.Services.Entity
{
    public class EntityUpdateRequest
    {
        public EntityAddOrUpdateViewModel ViewModel { get; set; }
        public Guid EntityId { get; set; }
        public IPrincipal RequestOwner { get; set; }
        public string SchemeHandle { get; set; }
    }
}