using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.DomainClasses.Users;

namespace VarinaCmsV2.ViewModel.User
{
    public class UserViewModel: BaseVeiwModel,IUrlableViewModel
    {
        public string Name { get; set; }
        public bool IsBanned { get; set; }
        public string BanReason { get; set; }
        public string Handle { get; set; }
        /// <summary>
        /// the primary identity of user like admin author provider of something ,....
        /// </summary>
        public string Title { get; set; }
        public string AvatarPath { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public List<Premission> AdditionalPermissions { get; set; }
        public List<RoleViewModel> Roles { get; set; }
        public string ToUrl { get; set; }
        public string ToFullUrl { get; set; }
    }
}
