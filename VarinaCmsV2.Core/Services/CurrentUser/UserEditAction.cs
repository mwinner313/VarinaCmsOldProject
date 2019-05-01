using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VarinaCmsV2.Core.Services.CurrentUser
{
    public enum UserEditAction
    {
        None = 0,
        EditInfo = 1,
        ChangePassword = 2,
        ChangeAvatar = 3,
    }
}
