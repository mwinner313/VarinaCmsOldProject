using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.DomainClasses.Entities.Widgets;
using VarinaCmsV2.ViewModel.Widget;

namespace VarinaCmsV2.AutoMapperProfiles
{
    public class WidgetProfile:Profile
    {
        public WidgetProfile()
        {
            CreateMap<WidgetContainer, WidgetContainerViewModel>();
            CreateMap<WidgetContainerAddOrUpdateViewModel, WidgetContainer>();
            CreateMap<Widget, WidgetViewModel>();
            CreateMap<WidgetAddOrUpdateViewModel, Widget>();
        }

       
    }

}
