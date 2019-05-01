using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.DomainClasses.Users;

namespace VarinaCmsV2.Core.Settings
{
    public class WebSitePolicies
    {
        public List<Role> DefaultNewUserRegisterRole { get; set; } = new List<Role>();
        public CommentPolicy CommentPolicy { get; set; }
        public List<Role> CommentPolicyUserInRoles { get; set; } = new List<Role>();
        public EShopReviewPolicy EShopReviewPolicy { get; set; }
        public List<Role> EShopReviewPolicyUserInRoles { get; set; } = new List<Role>();
    }

    public enum EShopReviewPolicy
    {
        EveryOne = 5,
        RegisteredUsers = 10,
        UsersInRoles = 15,
        BuiedUsers = 20,
        BuiedUsersInRoles = 25,
    }

    public enum CommentPolicy
    {
        EveryOne = 5,
        RegisteredUsers = 10,
        UsersInRoles = 15,
    }
}
