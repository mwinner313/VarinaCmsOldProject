using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using VarinaCmsV2.DomainClasses.Entities.EShop;
using VarinaCmsV2.DomainClasses.Entities.EShop.Orders;
using VarinaCmsV2.ViewModel.User;

namespace VarinaCmsV2.Core.Logic.Helpers
{
    public static class ShoppingCartMapHelper
    {
        public static List<ShoppingCartItemLiquidViewModel> MapToLiquidVeiwModel(this IEnumerable<ShoppingCartItem> items)
        {
            return Mapper.Map<List<ShoppingCartItemLiquidViewModel>>(items);
        }
        public static ShoppingCartItemLiquidViewModel MapToLiquidVeiwModel(this ShoppingCartItem item)
        {
            return Mapper.Map<ShoppingCartItemLiquidViewModel>(item);
        }
    }
}
