using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VarinaCmsV2.ViewModel.Eshop.Shipment
{
    public class ShipmentAddOrUpdateVeiwModel 
    {
        public string TrackingNumber { get; set; }
        public long? TotalWeight { get; set; }
        public string ShippedDate { get; set; }
        public string DeliveryDate { get; set; }
        public string AdminComment { get; set; }
    }
}
