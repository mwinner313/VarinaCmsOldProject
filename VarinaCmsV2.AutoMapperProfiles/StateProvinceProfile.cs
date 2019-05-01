using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using VarinaCmsV2.DomainClasses.Users;
using VarinaCmsV2.ViewModel.States;

namespace VarinaCmsV2.AutoMapperProfiles
{
    public class StateProvinceProfile :Profile
    {
        public StateProvinceProfile()
        {
            CreateMap<StateProvince, StateProvinceLiquidViewModel>();
            CreateMap<StateProvince, StateProvinceViewModel>();
        }

    }
}
