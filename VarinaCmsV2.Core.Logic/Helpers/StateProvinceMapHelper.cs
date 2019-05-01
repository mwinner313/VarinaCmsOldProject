using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using VarinaCmsV2.DomainClasses.Users;
using VarinaCmsV2.ViewModel.States;

namespace VarinaCmsV2.Core.Logic.Helpers
{
    public static class StateProvinceMapHelper
    {
        public static StateProvinceLiquidViewModel MapToLiquidVeiwModel(this StateProvince sp)
        {
            return Mapper.Map<StateProvinceLiquidViewModel>(sp);
        }
        public static List<StateProvinceLiquidViewModel> MapToLiquidVeiwModel(this List<StateProvince> sps)
        {
            return Mapper.Map<List<StateProvinceLiquidViewModel>>(sps);
        }
    }
}
