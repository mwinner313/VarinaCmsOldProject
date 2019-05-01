using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using VarinaCmsV2.Core.Infrastructure;
using VarinaCmsV2.DomainClasses.Entities;
using VarinaCmsV2.ViewModel;
using VarinaCmsV2.ViewModel.Eshop;
using VarinaCmsV2.ViewModel.Page;
using VarinaCmsV2.ViewModel.Tag;

namespace VarinaCmsV2.AutoMapperProfiles
{
    public class TagProfile:Profile
    {
        public TagProfile()
        {
            CreateMap<Tag, TagViewModel>();
            CreateMap<TagViewModel, Tag>();
            CreateMap<Tag, EntityTagLiquidViewModel>();
            CreateMap<Tag, PageTagLiquidViewModel>();
            CreateMap<Tag, ProductTagLiquidViewModel>();
            CreateMap<EntityTagLiquidViewModel, Tag>();
            CreateMap<PageTagLiquidViewModel, Tag>();
            CreateMap<ProductTagLiquidViewModel, Tag>();

        }
    }

  
}
