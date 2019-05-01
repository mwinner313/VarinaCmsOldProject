using AutoMapper;
using VarinaCmsV2.DomainClasses.Entities.EShop.Shipping;
using VarinaCmsV2.ViewModel.Eshop;
using VarinaCmsV2.ViewModel.Eshop.Orders;
using VarinaCmsV2.ViewModel.Eshop.Shipment;

namespace VarinaCmsV2.AutoMapperProfiles
{
    public class ShipmentProfile:Profile
    {
        public ShipmentProfile()
        {
            CreateMap<DeliveryDate, DeliveryDateViewModel>();
            CreateMap<DeliveryDate, DeliveryDateLiquidVeiwModel>();
            CreateMap<DeliveryDateViewModel, DeliveryDate>();
            CreateMap<ShippingMethod, ShippingMethodLiquidViewModel>();
            CreateMap<Shipment, ShipmentViewModel>();
            CreateMap<ShipmentAddOrUpdateVeiwModel, Shipment > ();
        }
    }
}
