using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PersianDate;
using VarinaCmsV2.Common;

using VarinaCmsV2.DomainClasses.Entities;
using VarinaCmsV2.ViewModel.Page;

namespace VarinaCmsV2.AutoMapperProfiles
{
    public class PageProfile:Profile
    {
        public PageProfile()
        {
            CreateMap<PageAddUpdateViewModel, Page>();
            CreateMap<Page, PageAddUpdateViewModel>();

            CreateMap<Page, PageViewModel>();

            CreateMap<PageViewModel, Page>();

            CreateMap<Page, PageLiquidViewModel>();
            CreateMap<PageLiquidViewModel, Page>();
        }
    }

 
}
