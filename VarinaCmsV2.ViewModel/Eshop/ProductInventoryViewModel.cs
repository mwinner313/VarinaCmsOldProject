using VarinaCmsV2.DomainClasses.Entities.EShop;

namespace VarinaCmsV2.ViewModel.Eshop
{
    public class ProductInventoryViewModel
    {
        public int StockQuantity { get; set; }
        public int NotifyAdminForQuantityBelow { get; set; }
        public InventoryTrackMethod TrackMethod { get; set; }
        public int MinStockQuantity { get; set; }
        public MinStockQuntityAction MinStockQuntityAction { get; set; }
        public int OrderMaximumQuantity { get; set; }
        public int OrderMinimumQuantity { get; set; }
        /// <summary>
        /// Gets or sets the comma separated list of allowed quantities. null or empty if any quantity is allowed
        /// </summary>
        public string AllowedQuantities { get; set; }
    }
}