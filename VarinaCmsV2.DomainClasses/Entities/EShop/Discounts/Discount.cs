using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.DomainClasses.Helpers;

namespace VarinaCmsV2.DomainClasses.Entities.EShop.Discounts
{
    public class Discount:BaseEntity, IAccessRestrictedItem
    {
        public DiscountLimitationType LimitationType { get; set; }
        public DiscountType DiscountType { get; set; }
        public ICollection<ProductCategory>  AppliedToCategories { get; set; }
        public ICollection<Product>  AppliedToProducts { get; set; }
        [Required]
        public string Name { get; set; }
        public bool UsePercentage { get; set; }
        public int DiscountPercentage { get; set; } 
        public long DiscountAmount { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool RequiresCouponCode { get; set; }
        public long? MinimumCartAmountRequirement { get; set; }
        public string CouponCode { get; set; }
        public CouponCodeType CouponCodeType { get; set; } = CouponCodeType.Single;
        public ICollection<CouponCode> CouponCodes { get; set; }
//        public Portal Portal { get; set; }
//        public Guid PortalId { get; set; }
        public int LimitationTimes { get; set; }
        public int? MaximumDiscountedQuantity { get; set; }
        public bool AppliedToSubCategories { get; set; }
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