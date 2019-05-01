using System;
using System.Security.Principal;

namespace VarinaCmsV2.Core.Services.UserManagement
{
    public class UserDeleteRequest:IServiceRequest
    {
        public IPrincipal RequestOwner { get; set; }
        public Guid UserId { get; set; }
    }
}