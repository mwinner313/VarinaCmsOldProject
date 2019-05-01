using System;
using System.Security.Principal;

namespace VarinaCmsV2.Core.Services.Page
{
    public class PageDeleteRequest
    {
        public Guid PageId { get; set; }
        public IPrincipal RequestOwner { get; set; }
    }
}