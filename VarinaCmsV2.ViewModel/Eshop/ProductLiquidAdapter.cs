using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using VarinaCmsV2.Common;
using VarinaCmsV2.Common.CustomFields;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.DomainClasses.Entities;
using VarinaCmsV2.DomainClasses.Entities.Enums;
using VarinaCmsV2.DomainClasses.Entities.EShop;
using VarinaCmsV2.ViewModel.Eshop.Discount;

namespace VarinaCmsV2.ViewModel.Eshop
{
    public class ProductLiquidAdapter:UrlableLiquidAdapter
    {
        private bool _canAddToCart;
        public Guid Id { get; set; }
        public bool IsDownLoadFile { get; set; }
        public bool HasSampleFile { get; set; }
        public DownLoadFileLiquidAdapter SampleFile { get; set; }
        public DownLoadFileLiquidAdapter File { get; set; }
        public long OldPrice { get; set; }
        public long Price { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
        public string MetaTitle { get; set; }
        public string Url { get; set; }

        public bool CanAddToCart
        {
            get => _canAddToCart && (Inventory.TrackMethod == InventoryTrackMethod.DoNotTrack ||
                                     Inventory.StockQuantity > 0)
                &&(!AvailableForPreOrder|| PreOrderAvailabilityStartDateTime<=DateTime.Now);
            set => _canAddToCart = value;
        }

        public bool HasEshantion { get; set; }
        public bool HasDiscount=> AppliedDiscounts.Any();
        public bool HasRequiredProducts { get; set; }
        public bool AutomaticallyAddRequiredProducts { get; set; }
        public bool CallForPrice { get; set; }
        public ProductType Type { get; set; }
        public InventoryLiquidViewModel Inventory { get; set; }
        public ProductShipmentLiquidViewModel Shipment { get; set; } 
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string FullDescription { get; set; }
        public bool AllowCustomerReviews { get; set; }
        public bool AvailableForPreOrder { get; set; }
        public DateTime? PreOrderAvailabilityStartDateTime { get; set; }
        public DateTime PublishDateTime { get; set; }
        public int VisitCount { get; set; }
        public bool HasCrossSelledProducts { get; set; }
        public int LikeCount { get; set; }
        public ProductCatLiquidVeiwModel PrimaryCategory { get; set; }
        public List<ProductCatLiquidVeiwModel> RelatedCategories { get; set; }
        public List<ProductLiquidAdapter> RequiredProducts { get; set; }
        public List<ProductLiquidAdapter> CrossSellings { get; set; }
        public List<ProductLiquidAdapter> UpSellings { get; set; }
        public List<ProductLiquidAdapter> AssociatedProducts { get; set; }
        public List<ProductReviewLiquidViewModel> ProductReviews { get; set; }
        public List<ProductPictureLiquidViewModel> Pictures { get; set; }
        public List<ProductLiquidAdapter> Eshantions { get; set; }
        public List<DiscountLiquidViewModel> AppliedDiscounts { get; set; }=new List<DiscountLiquidViewModel>();
        public List<DeliveryDateLiquidVeiwModel> DeliveryDates { get; set; }
        public List<ProductTagLiquidViewModel> Tags { get; set; }
        public List<Field> Fields { get; set; }
        public CustomFieldFactoryProvider<JObject> FieldFactoryProvider { set; get; }
        public ProductPictureLiquidViewModel PrimaryPicture { get; set; }

        public override object BeforeMethod(string method)
        {
            var field = GetField(method);
            if (field != null) return field.ToLiquid();

            //if (MetaAdapter.HasMeta(Metas, method)) return MetaAdapter.AdaptAsLiquid(Metas, method);

            return base.BeforeMethod(method);
        }
        private CustomField<JObject> GetField(string method)
        {
            var rawField = Fields.FirstOrDefault(x => x.FieldDefenition.Handle == method);
            if (rawField == null) return null;

            var fieldFactory = FieldFactoryProvider.GetFieldFactory(rawField.FieldDefenition.Type);
            var field = fieldFactory.LoadField(rawField.RawValue, rawField.FieldDefenition.Title, rawField.FieldDefenition.MetaData);
            return field;
        }
    }
}
