using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.DomainClasses.Entities.EShop.Orders;
using VarinaCmsV2.DomainClasses.Entities.EShop.Shipping;
using VarinaCmsV2.ViewModel.Eshop.Shipment;

namespace VarinaCmsV2.ViewModel.Eshop.Orders
{
    public class OrderAddOrUpdateViewModel:BaseVeiwModel
    {
        public ShippingStatus ShippingStatus { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public string Comment { get; set; }
        public ShipmentAddOrUpdateVeiwModel Shipment { get; set; }


    }
}
