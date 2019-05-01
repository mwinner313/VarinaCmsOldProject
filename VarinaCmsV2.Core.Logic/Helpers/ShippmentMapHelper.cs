using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.DomainClasses.Entities.EShop.Shipping;
using VarinaCmsV2.ViewModel.Eshop.Shipment;

namespace VarinaCmsV2.Core.Logic.Helpers
{
    public static class ShippmentMapHelper
    {
        public static ShippingMethodLiquidViewModel MapToLiquidViewModel(this ShippingMethod method)
        {
            return AutoMapper.Mapper.Map<ShippingMethodLiquidViewModel>(method);
        }
        public static List<ShippingMethodLiquidViewModel> MapToLiquidViewModel(this List<ShippingMethod> methods)
        {
            return AutoMapper.Mapper.Map<List<ShippingMethodLiquidViewModel>>(methods);
        }
    }
}
