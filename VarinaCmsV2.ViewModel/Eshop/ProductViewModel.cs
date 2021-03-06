﻿using System;
using System.Collections.Generic;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.DomainClasses.Entities;
using VarinaCmsV2.DomainClasses.Entities.Enums;
using VarinaCmsV2.DomainClasses.Entities.EShop;
using VarinaCmsV2.DomainClasses.Entities.EShop.Shipping;
using VarinaCmsV2.ViewModel.Entities;
using VarinaCmsV2.ViewModel.Eshop.Discount;
using VarinaCmsV2.ViewModel.Eshop.Shipment;
using VarinaCmsV2.ViewModel.FileData;
using VarinaCmsV2.ViewModel.User;

namespace VarinaCmsV2.ViewModel.Eshop
{
    public class ProductViewModel:BaseVeiwModel, IUrlableViewModel
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public long ProductCost { get; set; }
        public long OldPrice { get; set; }
        public long Price { get; set; }
        public bool IsDownLoadFile { get; set; }
        public Guid? FileId { get; set; }
        public Guid? SampleFileId { get; set; }
        public bool HasSampleFile { get; set; }
        public int? DownloadExpirationDays { get; set; }
        public bool UnlimitedDownloads { get; set; }
        public int MaxNumberOfDownloads { get; set; }
        public ProductDownLoadActivationType DownLoadActivationType { get; set; }
        public ProductShipmentViewModel Shipment { get; set; } 
        public ProductInventoryViewModel Inventory { get; set; }

        public string AllowedQuantities { get; set; }
        public bool CanAddToCart { get; set; }
        public bool VisibleIndividually { get; set; }
        public bool HasEshantion { get; set; }
        public bool HasCrossSelledProducts { get; set; }
        public bool CallForPrice { get; set; }
        public bool HasRequiredProducts { get; set; }
        public bool AutomaticallyAddRequiredProducts { get; set; }
        public ProductType Type { get; set; }
        public Guid? ParentGroupedProductId { get; set; }
        public string ShortDescription { get; set; }
        public string FullDescription { get; set; }
        public string AdminComment { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
        public string MetaTitle { get; set; }
        public bool AllowCustomerReviews { get; set; }
        /// <summary>
        /// ex : SHOE_FOOTBAL مقداری منحصر بفرد id محصول در انبار
        /// </summary>
        public string Sku { get; set; }
        public bool AvailableForPreOrder { get; set; }
        public string PreOrderAvailabilityStartDateTime { get; set; }
        public string UpdateDateTime { get; set; }
        public string CreateDateTime { get; set; }
        public string ToUrl { get; set; }
        public string ToFullUrl { get; set; }
        public Guid PrimaryCategoryId { get; set; }
        public string PrimaryCategoryTitle { get; set; }
        public string Handle { get; set; }
        public bool IsVisible { get; set; }
        public bool IsDeleted { get; set; }
        public RelatedProductLoadType RelatedProductLoadType { get; set; }
        public Guid CreatorId { get; set; }
        public List<AllowedRole> AllowedRoles { get; set; }
        public AccessType AccessType { get; set; }
        public string PublishDateTime { get; set; }
        public string AvailibleEndDateTime { get; set; }
        public int VisitCount { get; set; }
        public int LikeCount { get; set; }
        public string CreatorUserName { get; set; }
        public List<FieldViewModel> Fields { get; set; }
        public FileDataViewModelWithMeta File { get; set; }
        public FileDataViewModelWithMeta SampleFile { get; set; }
        public ProductCategoryViewModel PrimaryCategory { get; set; }
        public List<ProductCategoryViewModel> RelatedCategories { get; set; }
        public List<ProductSimpleModel> UpSellings { get; set; }
        public List<ProductSimpleModel> RequiredProducts { get; set; }
        public List<ProductSimpleModel> CrossSellings { get; set; }
        public List<ProductSimpleModel> AssociatedProducts { get; set; }
        public List<ProductSimpleModel> Eshantions { get; set; }

        public List<ProductReviewViewModel> ProductReviews { get; set; }
        public EntitySchemeViewModel Scheme { get; set; }
        public List<ProductPictureViewModel> Pictures { get; set; }
        public ProductPictureViewModel PrimaryImage { get; set; }
        public List<DeliveryDateViewModel> DeliveryDates { get; set; }
        public List<TagViewModel> Tags { get; set; }
       
    }
}
