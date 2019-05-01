using AutoMapper;
using VarinaCmsV2.DomainClasses.Entities.EShop.Orders;
using VarinaCmsV2.ViewModel.Eshop.Orders;

namespace VarinaCmsV2.AutoMapperProfiles
{
    public class OrderProfile:Profile
    {
        public OrderProfile()
        {

            CreateMap<Order, OrderLiquidViewModel>();
            CreateMap<OrderAddOrUpdateViewModel, Order > ();
            CreateMap<Order, OrderViewModel>();
            CreateMap<OrderItem, OrderItemLiquidViewModel>();
            CreateMap<OrderItem, OrderItemViewModel>();
            CreateMap<OrderLog, OrderLogViewModel>();
        }
    }
}
