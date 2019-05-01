using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.DomainClasses.Entities;
using VarinaCmsV2.ViewModel;
using VarinaCmsV2.ViewModel.Meta;

namespace VarinaCmsV2.AutoMapperProfiles
{
    public class MetaProfile:AutoMapper.Profile
    {
        public MetaProfile()
        {
            CreateMap<Meta, MetaDataViewModel>();
            CreateMap<MetaDataAddOrUpdateViewModel, Meta>();
            CreateMap<Meta, MetaDataLiquidViewMdoel>().ConstructUsingServiceLocator();
        }
    }
}
