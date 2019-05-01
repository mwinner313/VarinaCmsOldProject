using System;
using System.Security.Principal;

namespace VarinaCmsV2.Core.Services.Entity
{
    public class EntityDeleteRequest
    {
        public IPrincipal RequestOwner { get; set; }
        public Guid EntityId { get; set; }
        public string SchemeHandle { get; set; }
    }
}