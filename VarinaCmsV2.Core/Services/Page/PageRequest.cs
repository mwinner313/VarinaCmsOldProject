using System;
using System.Security.Principal;

namespace VarinaCmsV2.Core.Services.Page
{
    public class PageRequest
    {
        public IPrincipal RequestOwner { get; set; }
        public Guid PageId { get; set; }
    }
}