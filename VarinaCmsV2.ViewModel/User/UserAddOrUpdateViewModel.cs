using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.DomainClasses.Users;

namespace VarinaCmsV2.ViewModel.User
{
    public class UserAddOrUpdateViewModel:BaseVeiwModel
    {
        public string Name { get; set; }
        public bool IsBanned { get; set; }
        public string BanReason { get; set; }
        public string Handle { get; set; }
        public string AvatarPath { get; set; }
        public string Url { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public List<UserRoleAddOrUpdateModel>  Roles { get; set; }
        public List<Premission> AdditionalPermissions { get; set; }
        public string Password { get; set; }
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
