using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.ViewModel.User.Account;

namespace VarinaCmsV2.Core.Services.CurrentUser
{
    public class EditCurrentUserModel
    {
        public UserEditAction Action { get; set; }
        public ChangePasswordViewModel ChangePasswordViewModel { get; set; }
        public UserInfoEditViewModel UserInfoEditViewModel { get; set; }
        public HttpContent UserAvatar { get; set; }
    }
}
