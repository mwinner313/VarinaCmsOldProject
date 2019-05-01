using System;
using System.Security.Principal;

namespace VarinaCmsV2.Core.Services.Comments
{
    public class CommentDetailRequest
    {
        public Guid Id { get; set; }
        public IPrincipal RequestOwner { get; set; }
    }
}