using System;
using System.Security.Principal;
using VarinaCmsV2.ViewModel.User;

namespace VarinaCmsV2.Core.Services.UserManagement
{
    public class UserUpdateRequest:IServiceRequest
    {
        public IPrincipal RequestOwner { get; set; }
        public UserAddOrUpdateViewModel User { get; set; }
        public Guid UserId { get; set; }
    }
}