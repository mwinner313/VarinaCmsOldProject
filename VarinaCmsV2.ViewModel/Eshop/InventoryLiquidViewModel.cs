using DotLiquid;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.DomainClasses.Entities.EShop;

namespace VarinaCmsV2.ViewModel.Eshop
{
    public class InventoryLiquidViewModel:Drop
    {
        public int OrderMaximumQuantity { get; set; }
        public int OrderMinimumQuantity { get; set; }
        public int StockQuantity { get; set; }
        public InventoryTrackMethod TrackMethod { get; set; }

        public string AllowedQuantities { get; set; }
    }
}