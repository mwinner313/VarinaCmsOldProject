using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using VarinaCmsV2.DomainClasses.Entities;
using VarinaCmsV2.ViewModel.Category;
using VarinaCmsV2.ViewModel.Entities;

namespace VarinaCmsV2.AutoMapperProfiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, EntityCatLiquid>();
            CreateMap<EntityCatLiquid, Category>();
            CreateMap<Category, CategoryViewModel>().ForMember(x=>x.DisplayName,opt=>opt.MapFrom(s=>s.GetDisplayName()));
            CreateMap<CategoryAddOrUpdateViewModel, Category>().ForMember(x => x.Handle, opt => opt.MapFrom(prop =>
                     string.IsNullOrEmpty(prop.Handle) ? prop.Id.ToString() : prop.Handle
                   ))
                   .ForMember(x=>x.FieldDefenitions,opt=>opt.Ignore())
                   .ForMember(x=>x.FieldDefenitionGroups,opt=>opt.Ignore());
            CreateMap<RelatedCategoryViewModel, Category>();
            CreateMap<Category, CategoryLiquidViewModel>().ForMember(x => x.DisplayName, opt => opt.MapFrom(s => s.GetDisplayName()));
            CreateMap<CategoryLiquidViewModel, Category>();
        }
    }

    public static class CategoryDisplayNameHelper
    {
        public static string GetDisplayName(this Category cat)
        {
            if (cat.ParentId.HasValue) return $"{cat.Parent.GetDisplayName()} > {cat.Title}";
            return cat.Title;
        }
    }
}
