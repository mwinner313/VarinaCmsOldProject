using System.Security.Principal;
using VarinaCmsV2.Common;

namespace VarinaCmsV2.Core.Services.EntityScheme
{
    public class EntitySchemeListRequest
    {
       public EntitySchemeQuery Query { get; set; }
        public IPrincipal RequestOwner { get; set; }
    }
}