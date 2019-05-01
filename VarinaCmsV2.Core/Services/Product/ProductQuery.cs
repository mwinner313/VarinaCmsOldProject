using System;
using System.Collections.Generic;
using VarinaCmsV2.Core.Services.Entity;
using VarinaCmsV2.DomainClasses.Entities.Enums;
using VarinaCmsV2.DomainClasses.Entities.EShop;

namespace VarinaCmsV2.Core.Services.Product
{
    public class ProductQuery: EntityQuery
    {
        public string Sku { get; set; }
        public string Title { get; set; }
        public bool LowStocks { get; set; }
        public bool AvailibleForPreOrder { get; set; }
        public bool HasEshantion { get; set; }
        public List<Guid> EshantionIds { get; set; }
        public ProductTypeOption ProductType { get; set; }
        public InventoryTrackMethod InventoryTrackMethod { get; set; }
        public bool HasDiscountsApplied { get; set; }
    }
}