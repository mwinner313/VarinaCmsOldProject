using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using VarinaCmsV2.DomainClasses.Entities.EShop.Discounts;
using VarinaCmsV2.ViewModel.Eshop;
using VarinaCmsV2.ViewModel.Eshop.Discount;

namespace VarinaCmsV2.Core.Logic.Helpers
{
    public static class DiscountMapHelper
    {
        public static List<Discount> MapToModel(this List<DiscountViewModel> vms)
        {
            return Mapper.Map<List<Discount>>(vms);
        }
        public static List<Discount> MapToModel(this List<DiscountIdModel> vms)
        {
            return Mapper.Map<List<Discount>>(vms);
        }
        public static List<DiscountViewModel> MapToViewModel(this List<Discount> discounts)
        {
            return Mapper.Map<List<DiscountViewModel>>(discounts);
        }
        public static DiscountViewModel MapToViewModel(this Discount discounts)
        {
            return Mapper.Map<DiscountViewModel>(discounts);
        }
        public static Discount MapToModel(this DiscountAddOrUpdateModel discount)
        {
            return Mapper.Map<Discount>(discount);
        }
        public static void MapToExisting(this DiscountAddOrUpdateModel discount,Discount existing)
        {
             Mapper.Map(discount,existing);
        }

    }
}
