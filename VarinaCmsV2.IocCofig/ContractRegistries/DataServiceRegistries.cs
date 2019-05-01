using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StructureMap.Web;
using VarinaCmsV2.Core.Contracts.DataServices;

using VarinaCmsV2.DomainClasses.Entities;
using VarinaCmsV2.DomainClasses.Entities.EShop;
using VarinaCmsV2.DomainClasses.Entities.EShop.Discounts;
using VarinaCmsV2.DomainClasses.Entities.EShop.Orders;
using VarinaCmsV2.DomainClasses.Entities.Widgets;
using VarinaCmsV2.DomainClasses.Users;
using VarinaCmsV2.Services.DataServices;

namespace VarinaCmsV2.IocCofig.ContractRegistries
{
    public class DataServiceRegistries : StructureMap.Registry
    {
        public DataServiceRegistries()
        {
            For<IEntitySchemeDataService>().HybridHttpOrThreadLocalScoped().Use<EntitySchemeDataService>();
            For<ILanguageDataService>().HybridHttpOrThreadLocalScoped().Use<LanguageDataService>();
            For<IEntityDataService>().HybridHttpOrThreadLocalScoped().Use<EntityDataService>();
            For<ICategoryDataService>().HybridHttpOrThreadLocalScoped().Use<CategoryDataService>();
            For<IPageDataService>().HybridHttpOrThreadLocalScoped().Use<PageDataService>();
            For<IUserDataService>().HybridHttpOrThreadLocalScoped().Use<UserDataService>();
            For<ITagDataService>().HybridHttpOrThreadLocalScoped().Use<TagDataService>();
            For<IFieldDefenitionDataService>().HybridHttpOrThreadLocalScoped().Use<FieldDefenitionDataService>();
            For<IMenuDataService>().HybridHttpOrThreadLocalScoped().Use<MenuDataService>();
            For<IMenuItemDataService>().HybridHttpOrThreadLocalScoped().Use<MenuItemDataService>();
            For<IWidgetDataService>().HybridHttpOrThreadLocalScoped().Use<WidgetDataService>();
            For<IWidgetContainerDataService>().HybridHttpOrThreadLocalScoped().Use<WidgetContainerDataService>();
            For<IFormSchemeDataService>().HybridHttpOrThreadLocalScoped().Use<FormSchemeDataService>();
            For<IFormDataService>().HybridHttpOrThreadLocalScoped().Use<FormDataService>();
            For<IMetaDataDataService>().HybridHttpOrThreadLocalScoped().Use<MetaDataDataService>();
            For<IFileDataDataService>().HybridHttpOrThreadLocalScoped().Use<FileDataDataService>();
            For<ICommentDataService>().HybridHttpOrThreadLocalScoped().Use<CommentDataService>();
            For<IProductDataService>().HybridHttpOrThreadLocalScoped().Use<ProductDataService>();
            For<IProductCategoryDataService>().HybridHttpOrThreadLocalScoped().Use<ProductCategoryDataService>();
            For<IDiscountDataService>().HybridHttpOrThreadLocalScoped().Use<DiscountDataService>();
            For<IProductReviewDataService>().HybridHttpOrThreadLocalScoped().Use<ProductReviewDataService>();
            For<IOrderDataService>().HybridHttpOrThreadLocalScoped().Use<OrderDataService>();
            For<IOrderItemDataService>().HybridHttpOrThreadLocalScoped().Use<OrderItemDataService>();
            For<IOrderLogDataService>().HybridHttpOrThreadLocalScoped().Use<OrderLogDataService>();
            For<IDiscountUsageDataService>().HybridHttpOrThreadLocalScoped().Use<DiscountUsageDataService>();
            For<ISettingDataService>().HybridHttpOrThreadLocalScoped().Use<SettingDataService>();
            For<IShoppingCartItemDataService>().HybridHttpOrThreadLocalScoped().Use<ShoppingCartItemDataService>();
            For<IMessageServiceAccountDataService>().HybridHttpOrThreadLocalScoped().Use<MessageServiceAccountDataService>();
            For<ICouponCodeDataService>().HybridHttpOrThreadLocalScoped().Use<CouponCodeDataService>();
            For<IFieldDefenitionGroupDataService>().HybridHttpOrThreadLocalScoped().Use<FieldDefenitionGroupDataService>();
            For<IFieldDataService>().HybridHttpOrThreadLocalScoped().Use<FieldDataService>();
            For<IAddressDataService>().HybridHttpOrThreadLocalScoped().Use<AddressDataService>();

            For<IDataService<EntityScheme, Guid>>().HybridHttpOrThreadLocalScoped().Use<EntitySchemeDataService>();
            For<IDataService<Entity, Guid>>().HybridHttpOrThreadLocalScoped().Use<EntityDataService>();
            For<IDataService<Category, Guid>>().HybridHttpOrThreadLocalScoped().Use<CategoryDataService>();
            For<IDataService<Page, Guid>>().HybridHttpOrThreadLocalScoped().Use<PageDataService>();
            For<IDataService<Tag, Guid>>().HybridHttpOrThreadLocalScoped().Use<TagDataService>();
            For<IDataService<Menu, Guid>>().HybridHttpOrThreadLocalScoped().Use<MenuDataService>();
            For<IDataService<FieldDefenition, Guid>>().HybridHttpOrThreadLocalScoped().Use<FieldDefenitionDataService>();
            For<IDataService<User, Guid>>().HybridHttpOrThreadLocalScoped().Use<UserDataService>();
            For<IDataService<Widget, Guid>>().HybridHttpOrThreadLocalScoped().Use<WidgetDataService>();
            For<IDataService<WidgetContainer, Guid>>().HybridHttpOrThreadLocalScoped().Use<WidgetContainerDataService>();
            For<IDataService<FormScheme, Guid>>().HybridHttpOrThreadLocalScoped().Use<FormSchemeDataService>();
            For<IDataService<Meta, Guid>>().HybridHttpOrThreadLocalScoped().Use<MetaDataDataService>();
            For<IDataService<FileData, Guid>>().HybridHttpOrThreadLocalScoped().Use<FileDataDataService>();
            For<IDataService<Comment, Guid>>().HybridHttpOrThreadLocalScoped().Use<CommentDataService>();
            For<IDataService<Product, Guid>>().HybridHttpOrThreadLocalScoped().Use<ProductDataService>();
            For<IDataService<ProductCategory, Guid>>().HybridHttpOrThreadLocalScoped().Use<ProductCategoryDataService>();
            For<IDataService<Discount, Guid>>().HybridHttpOrThreadLocalScoped().Use<DiscountDataService>();
            For<IDataService<ProductReview, Guid>>().HybridHttpOrThreadLocalScoped().Use<ProductReviewDataService>();
            For<IDataService<OrderItem, Guid>>().HybridHttpOrThreadLocalScoped().Use<OrderItemDataService>();
            For<IDataService<Order, Guid>>().HybridHttpOrThreadLocalScoped().Use<OrderDataService>();
            For<IDataService<DiscountUsageHistory, Guid>>().HybridHttpOrThreadLocalScoped().Use<DiscountUsageDataService>();
            For<IDataService<Setting, Guid>>().HybridHttpOrThreadLocalScoped().Use<SettingDataService>();
            For<IDataService<ShoppingCartItem, Guid>>().HybridHttpOrThreadLocalScoped().Use<ShoppingCartItemDataService>();
            For<IDataService<FieldDefenitionGroup, Guid>>().HybridHttpOrThreadLocalScoped().Use<FieldDefenitionGroupDataService>();
            For<IDataService<Field, Guid>>().HybridHttpOrThreadLocalScoped().Use<FieldDataService>();
            For<IDataService<Address, Guid>>().HybridHttpOrThreadLocalScoped().Use<AddressDataService>();


        }
    }
}
