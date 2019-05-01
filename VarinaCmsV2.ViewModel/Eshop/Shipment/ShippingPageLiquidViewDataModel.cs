using System;
using System.Collections.Generic;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.ViewModel.States;
using VarinaCmsV2.ViewModel.User;

namespace VarinaCmsV2.ViewModel.Eshop.Shipment
{
    public class ShippingPageLiquidViewDataModel:LiquidAdapter
    {
        public Guid SelectedShippinMethodId { get; set; }
        public List<AddressLiquidViewModel> AvailibleAddresses { get; set; }
        public Guid? SelectedAddressId { get; set; }
        public List<ShippingMethodLiquidViewModel> ShippingMethods { get; set; }
        public List<ShoppingCartItemLiquidViewModel> ShippingItems { get; set; }
        public List<StateProvinceLiquidViewModel> StateProvinces { get; set; }
    }
}
