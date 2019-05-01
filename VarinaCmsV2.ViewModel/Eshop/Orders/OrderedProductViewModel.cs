using System;
using System.Collections.Generic;
using VarinaCmsV2.DomainClasses.Entities.Enums;

namespace VarinaCmsV2.ViewModel.Eshop.Orders
{
    public class OrderedProductViewModel
    {
        public string Title { get; set; }
        public Guid Id { get; set; }
        public bool IsDownLoadFile { get; set; }
        public int? DownloadExpirationDays { get; set; }
        public bool UnlimitedDownloads { get; set; }
        public ProductDownLoadActivationType DownLoadActivationType { get; set; } = ProductDownLoadActivationType.WhenPaid;
        public int MaxNumberOfDownloads { get; set; }
        public List<ProductPictureViewModel> Pictures { get; set; }
        public ProductPictureViewModel PrimaryImage { get; set; }
    }
}