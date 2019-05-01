using System.Collections.Generic;
using VarinaCmsV2.ViewModel.User;

namespace VarinaCmsV2.Core.Services.UserManagement
{
    public class UserListResponse:IServiceResponse
    {
        public ResponseAccess Access { get; set; }
        public string Message { get; set; } = "";
        public List<UserViewModel> Users { get; set; }
        public long Count { get; set; }
    }
}