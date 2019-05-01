using System;
using System.Collections.Generic;
using System.Security.Principal;

namespace VarinaCmsV2.Core.Services.Comments
{
    public class CommentApproveRequest:IServiceRequest
    {
        public IPrincipal RequestOwner { get; set; }
        public List<Guid> Ids { get; set; }
    }
}