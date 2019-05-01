using System.Data.Entity;
using System.Linq;
using AutoMapper;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.Core.Contracts.Web;
using VarinaCmsV2.DomainClasses;
using VarinaCmsV2.DomainClasses.Entities;
using VarinaCmsV2.DomainClasses.Entities.Enums;
using VarinaCmsV2.ViewModel.Menu;

namespace VarinaCmsV2.AutoMapperProfiles.Reslovers
{
    internal class MenuItemUrlResolver : IValueResolver<MenuItem, IMenuItemMapped, string>
    {
        private readonly ICmsUrlBuilderFactory _cmsUrlBuilderFactory;
        private readonly IPageDataService _pageDataService;
        private readonly IEntityDataService _entityDataService;
        private readonly IProductDataService _productDataService;
        private readonly IUserDataService _userDataService;
        private readonly ICategoryDataService _categoryDataService;
        private readonly IProductCategoryDataService _productCategoryDataService;
        public MenuItemUrlResolver(ICmsUrlBuilderFactory cmsUrlBuilderFactory, IPageDataService pageDataService, IEntityDataService entityDataService, IUserDataService userDataService, ICategoryDataService categoryDataService, IProductDataService productDataService, IProductCategoryDataService productCategoryDataService)
        {
            _cmsUrlBuilderFactory = cmsUrlBuilderFactory;
            _pageDataService = pageDataService;
            _entityDataService = entityDataService;
            _userDataService = userDataService;
            _categoryDataService = categoryDataService;
            _productDataService = productDataService;
            _productCategoryDataService = productCategoryDataService;
        }

        public string Resolve(MenuItem source, IMenuItemMapped destination, string destMember, ResolutionContext context)
        {
            switch (source.TargetType)
            {
                case MenuItemTargetType.CustomLink:
                    return source.Url;

                case MenuItemTargetType.Category:
                    var category = _categoryDataService.Query.Include(x=>x.Scheme).FirstOrDefault(x => x.Id == source.TargetId.Value);
                    if (category is null) return "item not found";
                    var caturl = _cmsUrlBuilderFactory.GetUrlBuilder(category).GenerateFull(category);
                    return caturl;

                case MenuItemTargetType.Entity:
                    var entity = _entityDataService.Query.Include(x => x.Scheme).FirstOrDefault(x => x.Id == source.TargetId.Value);
                    if (entity is null) return "item not found";
                    return _cmsUrlBuilderFactory.GetUrlBuilder(entity).GenerateFull(entity);

                case MenuItemTargetType.UserProfile:
                    var user = _userDataService.Query.FirstOrDefault(x => x.Id == source.TargetId.Value);
                    if (user is null) return "item not found";
                    return _cmsUrlBuilderFactory.GetUrlBuilder(user).GenerateFull(user);

                case MenuItemTargetType.Page:
                    var page = _pageDataService.Query.FirstOrDefault(x => x.Id == source.TargetId.Value);
                    if (page is null) return "item not found";
                    var url = _cmsUrlBuilderFactory.GetUrlBuilder(page).GenerateFull(page);
                    return url;

                case MenuItemTargetType.Product:
                    var product = _productDataService.Query.FirstOrDefault(x => x.Id == source.TargetId);
                    if (product is null) return "item not found";
                    return _cmsUrlBuilderFactory.GetUrlBuilder(product).GenerateFull(product);

                case MenuItemTargetType.ProductCategory:
                    var cat = _productCategoryDataService.Query.FirstOrDefault(x => x.Id == source.TargetId);
                    if (cat is null) return "item not found";
                    return _cmsUrlBuilderFactory.GetUrlBuilder(cat).GenerateFull(cat);
                default:
                    return "";
            }

        }
    }
}