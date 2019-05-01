using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using VarinaCmsV2.Core.EntityStates;
using VarinaCmsV2.ViewModel.Entities;

namespace VarinaCmsV2.AutoMapperProfiles
{
    public class EntitySchemeStateProfile : Profile
    {
        public EntitySchemeStateProfile()
        {
            CreateMap<EntitySchemeState, EntitySchemeStateViewModel>();
        }
    }
}
