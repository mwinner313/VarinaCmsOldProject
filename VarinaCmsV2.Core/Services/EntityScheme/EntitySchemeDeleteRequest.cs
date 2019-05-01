using System;
using System.Security.Principal;

namespace VarinaCmsV2.Core.Services.EntityScheme
{
    public class EntitySchemeDeleteRequest
    {
        public Guid Id { get; set; }
        public IPrincipal RequestOwner { get; set; }
    }
}