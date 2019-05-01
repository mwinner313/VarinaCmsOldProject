using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using VarinaCmsV2.Core.Services.CurrentUser;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.DomainClasses.Users;
using VarinaCmsV2.ViewModel.User;

namespace VarinaCmsV2.Core.Logic.Helpers
{
    public static class AddressMapHelper
    {
        public static AddressLiquidViewModel MapToLiquidViewModel(this Address addr)
        {
            return AutoMapper.Mapper.Map<AddressLiquidViewModel>(addr);
        }
        public static UserAddressesResponseLiquidViewModel MapToLiquidViewModel(this UserAddressesResponse addresses)
        {
            return AutoMapper.Mapper.Map<UserAddressesResponseLiquidViewModel>(addresses);
        }

        public static void MapToExiting(this AddressAddOrUpdateModel addr, Address existing)
        {
             AutoMapper.Mapper.Map(addr,existing);
        }
        public static List<AddressLiquidViewModel> MapToLiquidViewModel(this List<Address> addresses)
        {
            return AutoMapper.Mapper.Map<List<AddressLiquidViewModel>>(addresses);
        }

        public static Address MapToModel(this AddressAddOrUpdateModel addr)
        {
            return Mapper.Map<Address>(addr);
        }
    }
}
