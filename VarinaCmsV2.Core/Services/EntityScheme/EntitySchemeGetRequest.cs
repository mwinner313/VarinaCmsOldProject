using System;
using System.Security.Principal;

namespace VarinaCmsV2.Core.Services.EntityScheme
{
    public class EntitySchemeGetRequest
    {
        public IPrincipal RequestOwner { get; set; }
        public Guid Id { get; set; }
        public string Handle { get; set; }
    }
}