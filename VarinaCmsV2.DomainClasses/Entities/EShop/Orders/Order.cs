using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.DomainClasses.Entities.EShop.Discounts;
using VarinaCmsV2.DomainClasses.Entities.EShop.Shipping;
using VarinaCmsV2.DomainClasses.Helpers;
using VarinaCmsV2.DomainClasses.Users;

namespace VarinaCmsV2.DomainClasses.Entities.EShop.Orders
{
    public class Order:BaseEntity,ITrackibleItem,ISoftDeletibleItem,IAccessRestrictedItem
    {
      
        public DateTime? PaidDate { get; set; }
        public Guid? ShippingAddressId { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long OrderTrackNumber { get; set; }
        public bool IsDeleted { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public ShippingStatus ShippingStatus { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentTrackNumber { get; set; }
        public string ShippingMethod { get; set; }
        public long OrderDiscount { get; set; }
        public long OrderTotal { get; set; }
        public long OrderSubTotal { get; set; }
        public bool SeenByAdmin { get; set; }
        public string Comment { get; set; }
        public DateTime UpdateDateTime { get; set; }
        public DateTime CreateDateTime { get; set; }
        public Guid CreatorId { get; set; }
        #region Navigation

        public ICollection<DiscountUsageHistory> DiscountUsageHistories { get; set; } =
            new List<DiscountUsageHistory>();
        public ICollection<OrderItem> OrderItems { get; set; }=new List<OrderItem>();
        public Shipment Shipment { get; set; }
        public Address ShippingAddress { get; set; }
        public User Creator { get; set; }
//        public Portal Portal { get; set; }
//        public Guid PortalId { get; set; }
        public ICollection<OrderLog> OrderLogs { get; set; }
        #endregion
        [NotMapped]
        public ObservableCollection<AllowedRole> AllowedRoles
        {
            get => this.GetAllowedRoles();
            set => AllowedRolesString = JsonConvert.SerializeObject(value);
        }
        public string AllowedRolesString { get; set; }
        public AccessType AccessType { get; set; }
    }
}
