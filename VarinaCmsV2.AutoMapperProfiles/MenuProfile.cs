using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using VarinaCmsV2.AutoMapperProfiles.Reslovers;
using VarinaCmsV2.DomainClasses;
using VarinaCmsV2.DomainClasses.Entities;
using VarinaCmsV2.ViewModel.Menu;
using VarinaCmsV2.DomainClasses.Entities.Enums;
using VarinaCmsV2.ViewModel;

namespace VarinaCmsV2.AutoMapperProfiles
{
    public class MenuProfile : Profile
    {
        public MenuProfile()
        {
            CreateMap<Menu, MenuViewModel>();
            CreateMap<MenuItem, MenuItemViewModel>().ForMember(x => x.Url, opt => opt.ResolveUsing<MenuItemUrlResolver>());

            CreateMap<MenuItemAddUpdateViewModel, MenuItem>();
            CreateMap<MenuAddUpdateViewModel, Menu>();

            CreateMap<Menu, MenuLiquidViewModel>();
            CreateMap<MenuItem, MenuItemLiquidViewModel>()
                .ForMember(x => x.Url, opt => opt.ResolveUsing<MenuItemUrlResolver>())
                .ForMember(x => x.TargetType, opt => opt.ResolveUsing<TargetTypeResolver>());
            CreateMap<MenuItemLiquidViewModel, MenuItemLiquidDecorator>();

        }


    }

    internal class TargetTypeResolver : IValueResolver<MenuItem, MenuItemLiquidViewModel, string>
    {
        public string Resolve(MenuItem source, MenuItemLiquidViewModel destination, string destMember, ResolutionContext context)
        {
            switch (source.TargetType)
            {
                case MenuItemTargetType.Category: return nameof(MenuItemTargetType.Category);
                case MenuItemTargetType.CustomLink: return nameof(MenuItemTargetType.CustomLink);
                case MenuItemTargetType.Entity: return nameof(MenuItemTargetType.Entity);
                case MenuItemTargetType.Page: return nameof(MenuItemTargetType.Page);
                case MenuItemTargetType.Product: return nameof(MenuItemTargetType.Product);
                case MenuItemTargetType.UserProfile: return nameof(MenuItemTargetType.UserProfile);
                default: throw new ArgumentOutOfRangeException("menuitem targettype");
            }
        }
    }
}
