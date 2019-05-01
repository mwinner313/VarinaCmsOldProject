using System;
using System.Collections.Generic;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.ViewModel.Entities;

namespace VarinaCmsV2.ViewModel.User.Account
{
    public class UserLiquidViewModel : UrlableLiquidAdapter
    {
        public string Name { get; set; }
        /// <summary>
        /// the primary identity of user like admin author provider of something ,....
        /// </summary>
        public string Title { get; set; }

        public Guid Id { get; set; }
        public string AvatarPath { get; set; }
        public string Email { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public List<UserLiquidViewModel> Followers { get; set; }
        public List<UserLiquidViewModel> Following { get; set; }
        public List<EntityLiquidAdapter> Entities { get; set; }
        public List<ShoppingCartItemLiquidViewModel> ShoppingCartItems { get; set; }
        public string Url { get; set; }
    }

   
}
