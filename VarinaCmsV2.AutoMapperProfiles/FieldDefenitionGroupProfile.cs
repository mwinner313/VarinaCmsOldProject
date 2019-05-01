using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using VarinaCmsV2.DomainClasses.Entities;
using VarinaCmsV2.ViewModel.Entities;

namespace VarinaCmsV2.AutoMapperProfiles
{
    public class FieldDefenitionGroupProfile :Profile
    {
        public FieldDefenitionGroupProfile()
        {
            CreateMap<FieldDefenitionGroup, FieldDefenitionGroupViewModel>();
            CreateMap<FieldDefenitionGroup, FieldDefenitionGroupLiquidVeiwModel>();
            CreateMap<FieldDefenitionGroupViewModel, FieldDefenitionGroup>();
            CreateMap<FieldDefenitionGroupAddOrUpdateModel, FieldDefenitionGroup>();
        }
    }
}
