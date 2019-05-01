using System.Security.Principal;

namespace VarinaCmsV2.Core.Services.UserManagement
{
    public class UserListRequest:IServiceRequest
    {
        public IPrincipal RequestOwner { get; set; }
        public UserQuery Query { get; set; }
    }
}