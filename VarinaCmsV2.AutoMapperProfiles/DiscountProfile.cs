using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using VarinaCmsV2.DomainClasses.Entities.EShop.Discounts;
using VarinaCmsV2.ViewModel.Eshop;
using VarinaCmsV2.ViewModel.Eshop.Discount;
using VarinaCmsV2.ViewModel.Eshop.Orders;

namespace VarinaCmsV2.AutoMapperProfiles
{
    public class DiscountProfile:Profile
    {
        public DiscountProfile()
        {
            CreateMap<Discount, DiscountViewModel>();
            CreateMap<Discount, DiscountLiquidViewModel>();
            CreateMap<DiscountIdModel, DiscountViewModel>();
            CreateMap<DiscountViewModel, Discount>();
            CreateMap<DiscountUsageHistory, DiscountUsageHistoryLiquidViewModel>();
            CreateMap<DiscountUsageHistory, DiscountUsageHistoryViewModel>();
            CreateMap<DiscountAddOrUpdateModel,Discount>();
        }
    }
}
