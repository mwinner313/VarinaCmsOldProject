using System;
using System.Collections.Generic;
using System.Security.Principal;

namespace VarinaCmsV2.Core.Services.Comments
{
    public class CommentDeleteRequest:IServiceRequest
    {
        public List<Guid> Ids { get; set; }
        public IPrincipal RequestOwner { get; set; }
    }
}