using System;
using System.Security.Principal;
using VarinaCmsV2.Common;

namespace VarinaCmsV2.Core.Services.Entity
{
    public class EntityListRequest
    {
        public IPrincipal RequestOwner { get; set; }
        public EntityQuery Query { get; set; }
    }
}