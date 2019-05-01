using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.DomainClasses.Entities.EShop.Orders;
using VarinaCmsV2.ViewModel.Eshop.Orders;

namespace VarinaCmsV2.Core.Logic.Helpers
{
    public static class OrderHelper
    {
        public static OrderLiquidViewModel MapToLiquidViewModel(this Order order)
        {
            return Mapper.Map<OrderLiquidViewModel>(order);
        }
        public static OrderViewModel MapToViewModel(this Order order)
        {
            return Mapper.Map<OrderViewModel>(order);
        }
        public static List<OrderViewModel> MapToViewModel(this List<Order> order)
        {
            return Mapper.Map<List<OrderViewModel>>(order);
        }
        public static LiquidList<OrderLiquidViewModel> MapToLiquidViewModel(this List<Order> order)
        {
            return Mapper.Map<LiquidList<OrderLiquidViewModel>>(order);
        }
        public static List<OrderLogViewModel> MapToViewModel(this List<OrderLog> order)
        {
            return Mapper.Map<List<OrderLogViewModel>>(order);
        }
        public static Order MapToModel(this OrderAddOrUpdateViewModel order)
        {
            return Mapper.Map<Order>(order);
        }
        public static void MapToExisting(this OrderAddOrUpdateViewModel order, Order exiting)
        {
            Mapper.Map(order, exiting);
        }
       

    }
}
