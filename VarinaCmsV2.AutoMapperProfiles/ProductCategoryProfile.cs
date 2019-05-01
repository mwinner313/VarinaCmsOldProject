using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using VarinaCmsV2.DomainClasses.Entities.EShop;
using VarinaCmsV2.ViewModel.Eshop;

namespace VarinaCmsV2.AutoMapperProfiles
{
    public class ProductCategoryProfile : Profile
    {
        public ProductCategoryProfile()
        {
            CreateMap<ProductCategory, ProductCategoryViewModel>().ForMember(x => x.DisplayName, opt => opt.MapFrom(s => s.GetDisplayName())); ;
            CreateMap<ProductCategoryIdModel, ProductCategory>();
            CreateMap<ProductCategoryAddOrUpdateModel, ProductCategory>().ForMember(x => x.Handle, opt => opt.MapFrom(prop =>
                string.IsNullOrEmpty(prop.Handle) ? prop.Id.ToString() : prop.Handle
            ))
            .ForMember(x => x.FieldDefenitions, opt => opt.Ignore())
            .ForMember(x => x.FieldDefenitionGroups, opt => opt.Ignore());
            CreateMap<ProductCategoryViewModel, ProductCategory>();
            CreateMap<ProductCategory, ProductCatLiquidVeiwModel>();
            CreateMap<ProductCategory, ProductCategoryLiquidVeiwModel>();
        }
    }
    public static class ProductCategoryDisplayNameHelper
    {
        public static string GetDisplayName(this ProductCategory cat)
        {
            if (cat.ParentId.HasValue) return $"{cat.Parent.GetDisplayName()} > {cat.Title}";
            return cat.Title;
        }
    }
}
