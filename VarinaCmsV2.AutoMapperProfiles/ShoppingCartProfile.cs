using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using VarinaCmsV2.DomainClasses.Entities.EShop;
using VarinaCmsV2.DomainClasses.Entities.EShop.Orders;
using VarinaCmsV2.ViewModel.User;

namespace VarinaCmsV2.AutoMapperProfiles
{
    public class ShoppingCartProfile:Profile
    {
        public ShoppingCartProfile()
        {
            CreateMap<ShoppingCartItem, ShoppingCartItemLiquidViewModel>();
        }
    }
}
