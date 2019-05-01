using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Sockets;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.DomainClasses.Entities;
using VarinaCmsV2.DomainClasses.Entities.EShop;
using VarinaCmsV2.DomainClasses.Entities.EShop.Orders;
using VarinaCmsV2.DomainClasses.Helpers;

namespace VarinaCmsV2.DomainClasses.Users
{
    public class User : IdentityUser<Guid, UserLogin, UserRole, UserClaim>, IUser<Guid>, IUrlable, IAccessRestrictedItem, IBaseEntity, IOptionalTrackibleItem, ISoftDeletibleItem
    {
        public User()
        {
            Id = Guid.NewGuid();
        }

        #region Navigation Props
        public User Creator { get; set; }
        public ICollection<User> Followers { get; set; }
        public ICollection<User> Following { get; set; }
        public ICollection<Entity> CreatedEntities { get; set; }
        public ICollection<ShoppingCartItem> ShoppingCartItems { get; set; }
        //public Portal Portal { get; set; }
        //public Guid PortalId { get; set; }
        public ICollection<Address> Addresses { get; set; } = new List<Address>();
        public virtual ICollection<UserAttribute> Attributes { get; set; }

        #endregion

        #region Props
        /// <summary>
        /// the primary identity of user like admin author provider of something ,....
        /// </summary>
        public string Title { get; set; }
        public string Name { get; set; }
        public string AllowedRolesString { get; set; }
        public bool IsBanned { get; set; }
        public DateTime UpdateDateTime { get; set; }
        public DateTime CreateDateTime { get; set; }
        public string BanReason { get; set; }

        public string Description { get; set; }
        public string Handle { get; set; }
        public AccessType AccessType { get; set; }
        public bool IsDeleted { get; set; }
        public string Url { get; set; }
        public string AvatarPath { get; set; }
        public string AdditionalPermissionsJobject { get; set; }
        #endregion
        public Guid? CreatorId { get; set; }

        [NotMapped]
        public ObservableCollection<Premission> AdditionalPermissions
        {
            get => this.GetAdditionalPremissions();
            set => AdditionalPermissionsJobject = JsonConvert.SerializeObject(value);
        }
        [NotMapped]
        public ObservableCollection<AllowedRole> AllowedRoles
        {
            get => this.GetAllowedRoles();
            set => AllowedRolesString = JsonConvert.SerializeObject(value);
        }

    }
}
