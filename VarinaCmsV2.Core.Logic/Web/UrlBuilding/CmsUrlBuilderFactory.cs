using VarinaCmsV2.Core.Contracts.Web;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.DomainClasses.Entities;
using VarinaCmsV2.DomainClasses.Entities.EShop;
using VarinaCmsV2.DomainClasses.Users;

namespace VarinaCmsV2.Core.Logic.Web.UrlBuilding
{
    public class CmsUrlBuilderFactory : ICmsUrlBuilderFactory
    {
        public virtual ICmsUrlBuilder GetUrlBuilder(IUrlable item)
        {
            
            if (item is User) return new UserProfileUrlBuilder();
            if (item is Entity) return new EntityUrlBuilder();
            if (item is Category) return new CategoryUrlBuilder();
            if (item is Page) return new PageUrlBuilder();
            if (item is Product) return new ProductUrlBuilder();
            if (item is ProductCategory) return new ProductCategoryUrlBuilder();
            if ((item as Tag)?.SchemeId != null) return new EntityTagUrlBuilder();
            if ((item as Tag)?.SchemeId == null) return new TagUrlBuilder();

            return null;
        }
    }
}
