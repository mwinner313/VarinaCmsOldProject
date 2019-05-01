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
    public class FieldDefenitionProfile:Profile
    {
        public FieldDefenitionProfile()
        {
            CreateMap<FieldDefenition, FieldDefenitionViewModel>();
            CreateMap<FieldDefenition, FieldDefenitionLiquidVeiwModel>();
            CreateMap<FieldDefenitionViewModel, FieldDefenition>();
            CreateMap<FieldDefenition, FieldDefenitionAddOrUpdateModel>();
            CreateMap<FieldDefenitionAddOrUpdateModel, FieldDefenition>();
            CreateMap<FieldDefenitionAddOrUpdateModel, FieldDefenition>();
        }
    }
}
