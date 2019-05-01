using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.DomainClasses.Entities.Enums;
using VarinaCmsV2.DomainClasses.Entities.EShop.Discounts;
using VarinaCmsV2.DomainClasses.Entities.EShop.Orders;
using VarinaCmsV2.DomainClasses.Entities.EShop.Shipping;
using VarinaCmsV2.DomainClasses.Helpers;
using VarinaCmsV2.DomainClasses.Users;

namespace VarinaCmsV2.DomainClasses.Entities.EShop
{
    public class Product : BaseEntity, ITemplatedItem, IVisibliltyControlled,
        ITaggedItem,
        ISoftDeletibleItem,
        ITrackibleItem,
        IAccessRestrictedItem,
        IHaveRelatedCategoryItem<ProductCategory>,
        IScheduledItem,
        IUrlable,
        IHaveMetaData, IEntity<ProductCategory>
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public ProductShipment Shipment { get; set; } = new ProductShipment();
        public Inventory Inventory { get; set; } = new Inventory();

        /// <summary>
        /// این قیمت به کاربر نهایی نشان داده نخواهد شد و فقط برای بحث حساب داری
        ///استفاده خواهد شد 
        /// </summary>
        public long ProductCost { get; set; }
        public long OldPrice { get; set; }
        public long Price { get; set; }

        #region Download
        public bool IsDownLoadFile { get; set; }
        public FileData File { get; set; }
        [ForeignKey(nameof(File))]
        public Guid? FileId { get; set; }
        [ForeignKey(nameof(SampleFile))]
        public Guid? SampleFileId { get; set; }
        public bool HasSampleFile { get; set; }
        public ProductDownLoadActivationType DownLoadActivationType { get; set; } = ProductDownLoadActivationType.WhenPaid;
        public int? DownloadExpirationDays { get; set; }
        public FileData SampleFile { get; set; }
        public bool UnlimitedDownloads { get; set; }
        public int MaxNumberOfDownloads { get; set; }
        #endregion
        #region seo
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
        public string MetaTitle { get; set; }
        #endregion

        public bool CanAddToCart { get; set; }
        /// <summary>
        ///  در صورت غیر فعال بود این گزینه این محصول بطور مستقیم در سیستم قابل مشاهده نخواهد بود
        /// </summary>
        public bool VisibleIndividually { get; set; }
        public bool HasEshantion { get; set; }
        /// <summary>
        /// performance optimization => AppliedDiscounts.Count > 0
        /// </summary>
        public bool CallForPrice { get; set; }
        public bool HasRequiredProducts { get; set; }
        public bool AutomaticallyAddRequiredProducts { get; set; }
        public ProductType Type { get; set; }
        public Guid? ParentGroupedProductId { get; set; }
        public string ShortDescription { get; set; }
        public string FullDescription { get; set; }
        public string AdminComment { get; set; }
        public bool AllowCustomerReviews { get; set; }
        /// <summary>
        /// ex : SHOE_FOOTBAL مقداری منحصر بفرد id محصول در انبار
        /// </summary>
        public string Sku { get; set; }
        public bool AvailableForPreOrder { get; set; }
        public DateTime? PreOrderAvailabilityStartDateTime { get; set; }
        public DateTime UpdateDateTime { get; set; } = DateTime.Now;
        public DateTime CreateDateTime { get; set; } = DateTime.Now;
        public Guid PrimaryCategoryId { get; set; }
        public string Handle { get; set; }
        public bool IsVisible { get; set; }
        public bool IsDeleted { get; set; }
        public bool HasCrossSelledProducts { get; set; }

        public Guid CreatorId { get; set; }
        [NotMapped]
        public ObservableCollection<AllowedRole> AllowedRoles
        {
            get => this.GetAllowedRoles();
            set => AllowedRolesString = JsonConvert.SerializeObject(value);
        }
        public string AllowedRolesString { get; set; }
        public AccessType AccessType { get; set; }
        public DateTime PublishDateTime { get; set; } = DateTime.Now;
        public DateTime? AvailibleEndDateTime { get; set; }
        public RelatedProductLoadType RelatedProductLoadType { get; set; }
        public int VisitCount { get; set; }
        public int LikeCount { get; set; }
        public Guid SchemeId { get; set; }
        #region navigation
        public User Creator { get; set; }
        public ProductCategory PrimaryCategory { get; set; }
        public Product ParentGrouped { get; set; }
        public ICollection<ProductCategory> RelatedCategories { get; set; }
        public ICollection<Discount> AppliedDiscounts { get; set; }
        public ICollection<Product> RequiredProducts { get; set; }
        public ICollection<Product> CrossSellings { get; set; }
        public ICollection<Product> UpSellings { get; set; }
        public ICollection<Product> AssociatedProducts { get; set; }
        public ICollection<ProductReview> ProductReviews { get; set; } = new List<ProductReview>();
        public ICollection<Field> Fields { get; set; }
        public EntityScheme Scheme { get; set; }
        public ICollection<ProductPicture> Pictures { get; set; }=new List<ProductPicture>();
        public ICollection<Product> Eshantions { get; set; }
        public ICollection<DeliveryDate> DeliveryDates { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        public ICollection<Tag> Tags { get; set; }
//        public Portal Portal { get; set; }
//        public Guid PortalId { get; set; }
        #endregion
    }
}
