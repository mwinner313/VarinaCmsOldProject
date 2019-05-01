using AutoMapper;
using VarinaCmsV2.AutoMapperProfiles.Reslovers;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.DomainClasses.Entities;
using VarinaCmsV2.DomainClasses.Entities.EShop;
using VarinaCmsV2.DomainClasses.Users;
using VarinaCmsV2.ViewModel;
using VarinaCmsV2.ViewModel.Category;
using VarinaCmsV2.ViewModel.Entities;
using VarinaCmsV2.ViewModel.Eshop;
using VarinaCmsV2.ViewModel.Page;
using VarinaCmsV2.ViewModel.Tag;
using VarinaCmsV2.ViewModel.User;
using VarinaCmsV2.ViewModel.User.Account;

namespace VarinaCmsV2.AutoMapperProfiles.InterfaceProfiles
{
    public class UrlableItemProfile : Profile
    {
        public UrlableItemProfile()
        {
            CreateMap<IUrlable, IUrlableViewModel>()
                .ForMember(x => x.ToUrl, opt => opt.ResolveUsing<CmsUrlBuilderResolver>())
                .ForMember(x => x.ToFullUrl, opt => opt.ResolveUsing<CmsFullUrlBuilderResolver>())
                .Include<Page, PageLiquidViewModel>()
                .Include<Tag, EntityTagLiquidViewModel>()
                .Include<Entity, EntityLiquidAdapter>()
                .Include<Category, EntityCatLiquid>()
                .Include<User, UserLiquidViewModel>()
                .Include<Page, PageViewModel>()
                .Include<Tag, TagViewModel>()
                .Include<Entity, EntityViewModel>()
                .Include<Category, CategoryViewModel>()
                .Include<User, UserViewModel>()
                //.Include<Product,ProductViewModel>()
                .Include<Product,ProductLiquidAdapter>()
                //.Include<ProductCategory, ProductCategoryViewModel>()
                .Include<ProductCategory, ProductCategoryLiquidVeiwModel>()
                .Include<ProductCategory, ProductCatLiquidVeiwModel>()
                .Include<User, UserWebClientViewModel>();
        }
    }
}
