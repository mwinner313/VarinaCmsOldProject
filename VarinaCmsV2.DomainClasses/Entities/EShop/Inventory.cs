

using System.ComponentModel.DataAnnotations.Schema;

namespace VarinaCmsV2.DomainClasses.Entities.EShop
{
    [ComplexType]
    public class Inventory
    {
        public int StockQuantity { get; set; }
        public int ReservedQuantity { get; set; }
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