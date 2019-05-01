using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VarinaCmsV2.DomainClasses.Entities.Enums;
using VarinaCmsV2.DomainClasses.Entities.EShop;
using VarinaCmsV2.ViewModel.Entities;
using VarinaCmsV2.ViewModel.Eshop.Discount;

namespace VarinaCmsV2.ViewModel.Eshop
{
    public class ProductAddOrUpdateModel:IValidatableObject, IEntityViewModel
    {
        [Required]
        public string Title { get; set; }
        public string Url { get; set; }
        public long ProductCost { get; set; }
        public long OldPrice { get; set; }
        [Required]
        public RelatedProductLoadType RelatedProductLoadType { get; set; }
        public long Price { get; set; }
        public bool IsDownLoadFile { get; set; }
        public Guid? FileId { get; set; }
        public Guid? SampleFileId { get; set; }
        public bool HasSampleFile { get; set; }
        public ProductDownLoadActivationType DownLoadActivationType { get; set; }
        public int? DownloadExpirationDays { get; set; }
        public bool UnlimitedDownloads { get; set; }
        public int MaxNumberOfDownloads { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
        public string MetaTitle { get; set; } [Required]
        public ProductShipmentViewModel Shipment { get; set; } = new ProductShipmentViewModel();
        public ProductInventoryViewModel Inventory { get; set; } = new ProductInventoryViewModel();
        public bool CanAddToCart { get; set; }
        public bool VisibleIndividually { get; set; }
        public bool HasEshantion { get; set; }
        public bool HasDiscountsApplied { get; set; }
        public bool CallForPrice { get; set; }
        public bool HasRequiredProducts { get; set; }
        public bool AutomaticallyAddRequiredProducts { get; set; }
        [Required]
        public ProductType Type { get; set; }
        public string ShortDescription { get; set; }
        public string FullDescription { get; set; }
        public string AdminComment { get; set; }
        public bool AllowCustomerReviews { get; set; }
        public string Sku { get; set; }
        public bool AvailableForPreOrder { get; set; }
        public string PreOrderAvailabilityStartDateTime { get; set; }
        [Required]
        public Guid PrimaryCategoryId { get; set; }
        public string Handle { get; set; }
        public bool IsVisible { get; set; }
        public bool HasCrossSelledProducts { get; set; }
        public string PublishDateTime { get; set; }
        public string AvailibleEndDateTime { get; set; }
        [Required]
        public Guid SchemeId { get; set; }
        public List<FieldAddOrUpdateViewModel> Fields { get; set; }
        public List<ProductCategoryIdModel> RelatedCategories { get; set; }
        public List<DiscountIdModel> AppliedDiscounts { get; set; }
        public List<ProductIdModel> RequiredProducts { get; set; }
        public List<ProductIdModel> CrossSellings { get; set; }
        public List<ProductIdModel> UpSellings { get; set; }
        public List<ProductIdModel> AssociatedProducts { get; set; }
        public List<ProductPictureViewModel> Pictures { get; set; }
        public List<ProductIdModel> Eshantions { get; set; }
        public List<TagViewModel> Tags { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (IsDownLoadFile && !UnlimitedDownloads &&  MaxNumberOfDownloads==0) { yield return new ValidationResult("MaxNumberOfDownloads not specified");}
        }
    }
}
