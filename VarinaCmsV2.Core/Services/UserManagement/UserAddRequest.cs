using System.Security.Principal;
using VarinaCmsV2.ViewModel.User;

namespace VarinaCmsV2.Core.Services.UserManagement
{
    public class UserAddRequest:IServiceRequest
    {
        public UserAddOrUpdateViewModel User { get; set; }
        public IPrincipal RequestOwner { get; set; }
    }
}