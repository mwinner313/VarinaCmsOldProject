using VarinaCmsV2.ViewModel.User;

namespace VarinaCmsV2.Core.Services.UserManagement
{
    public class UserResponse:IServiceResponse
    {
        public ResponseAccess Access { get; set; }
        public UserViewModel User { get; set; }
        public string Message { get; set; } = "";
    }
}