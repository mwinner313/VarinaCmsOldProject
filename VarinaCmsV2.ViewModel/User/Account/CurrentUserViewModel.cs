using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VarinaCmsV2.ViewModel.User.Account
{
    public class CurrentUserViewModel : BaseVeiwModel
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string AvatarPath { get; set; }
        public List<string> Premissions { get; set; }
        public List<string> Roles { get; set; }
    }
}
