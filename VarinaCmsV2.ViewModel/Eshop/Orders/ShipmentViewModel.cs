namespace VarinaCmsV2.ViewModel.Eshop.Orders
{
    public class ShipmentViewModel
    {
        public string TrackingNumber { get; set; }
        public long? TotalWeight { get; set; }
        public string ShippedDate { get; set; }
        public string DeliveryDate { get; set; }
        public string AdminComment { get; set; }
    }
}