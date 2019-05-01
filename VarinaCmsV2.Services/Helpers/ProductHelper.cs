using System.Linq;
using VarinaCmsV2.Common;
using VarinaCmsV2.DomainClasses.Entities.EShop;

namespace VarinaCmsV2.Services.Helpers
{
    public static class ProductHelper
    {
        public static int GetMinimumCartQuantity(this Product product)
        {
            if (string.IsNullOrEmpty(product.Inventory.AllowedQuantities)) return 1;
            return product.Inventory.AllowedQuantities.ParseNumbers().Min();
        }

        public static bool IsRequiredProductOutOfStock(this Product product)
        {
            return product.RequiredProducts.Any(x =>
                x.Inventory.StockQuantity < x.GetMinimumCartQuantity() &&
                x.Inventory.TrackMethod == InventoryTrackMethod.TrackByAvalibleQuantity);
        }
    }
}