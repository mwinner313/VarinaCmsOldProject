using System.Collections.Generic;
using DotLiquid.Tags;
using StructureMap.Web;
using VarinaCmsV2.Core.Logic.ServiceObservers;
using VarinaCmsV2.Core.Logic.Services;
using VarinaCmsV2.Core.Logic.Services.Category;
using VarinaCmsV2.Core.Logic.Services.Comment;
using VarinaCmsV2.Core.Logic.Services.CurrentUser;
using VarinaCmsV2.Core.Logic.Services.Dashboard;
using VarinaCmsV2.Core.Logic.Services.Discounts;
using VarinaCmsV2.Core.Logic.Services.Entitiy;
using VarinaCmsV2.Core.Logic.Services.EntityScheme;
using VarinaCmsV2.Core.Logic.Services.FileData;
using VarinaCmsV2.Core.Logic.Services.Form;
using VarinaCmsV2.Core.Logic.Services.FormScheme;
using VarinaCmsV2.Core.Logic.Services.Menu;
using VarinaCmsV2.Core.Logic.Services.Orders;
using VarinaCmsV2.Core.Logic.Services.Page;
using VarinaCmsV2.Core.Logic.Services.ProductCategories;
using VarinaCmsV2.Core.Logic.Services.Products;
using VarinaCmsV2.Core.Logic.Services.Settings;
using VarinaCmsV2.Core.Logic.Services.UserManagement;
using VarinaCmsV2.Core.ServiceObservers;
using VarinaCmsV2.Core.Services.Category;
using VarinaCmsV2.Core.Services.Comments;
using VarinaCmsV2.Core.Services.CurrentUser;
using VarinaCmsV2.Core.Services.Dashboard;
using VarinaCmsV2.Core.Services.Discounts;
using VarinaCmsV2.Core.Services.Entity;
using VarinaCmsV2.Core.Services.EntityScheme;
using VarinaCmsV2.Core.Services.File;
using VarinaCmsV2.Core.Services.Form;
using VarinaCmsV2.Core.Services.FormScheme;
using VarinaCmsV2.Core.Services.Menu;
using VarinaCmsV2.Core.Services.Orders;
using VarinaCmsV2.Core.Services.Page;
using VarinaCmsV2.Core.Services.Product;
using VarinaCmsV2.Core.Services.ProductCategories;
using VarinaCmsV2.Core.Services.Settings;
using VarinaCmsV2.Core.Services.UserManagement;
using VarinaCmsV2.Core.Services.Widget;

namespace VarinaCmsV2.IocCofig.ContractRegistries
{
    public class ServicesRegistries : StructureMap.Registry
    {
        public ServicesRegistries()
        {

            For<IEntityService>().HybridHttpOrThreadLocalScoped().Use<EntityService>();
            For<IEntitySchemeService>().HybridHttpOrThreadLocalScoped().Use<EntitySchemeService>();
            For<ICategoryService>().HybridHttpOrThreadLocalScoped().Use<CategoryService>();
            For<ICommentService>().HybridHttpOrThreadLocalScoped().Use<CommentService>();
            For<IPageService>().HybridHttpOrThreadLocalScoped().Use<PageService>();
            For<IProductService>().HybridHttpOrThreadLocalScoped().Use<ProductService>();
            For<IProductCategoryService>().HybridHttpOrThreadLocalScoped().Use<ProductCategoryService>();
            For<IUserManagementService>().HybridHttpOrThreadLocalScoped().Use<UserManagementService>();
            For<IMenuService>().HybridHttpOrThreadLocalScoped().Use<MenuService>();
            For<IWidgetContainerService>().HybridHttpOrThreadLocalScoped().Use<WidgetContainerService>();
            For<IFormSchemeService>().HybridHttpOrThreadLocalScoped().Use<FormSchemeService>();
            For<IFormService>().HybridHttpOrThreadLocalScoped().Use<FormService>();
            For<ICurrentUserService>().HybridHttpOrThreadLocalScoped().Use<CurrentUserService>();
            For<IOrderService>().HybridHttpOrThreadLocalScoped().Use<OrderService>();
            For<IOrderLogger>().HybridHttpOrThreadLocalScoped().Use<OrderLogger>();
            For<IDiscountService>().HybridHttpOrThreadLocalScoped().Use<DiscountService>();
            For<ISettingService>().HybridHttpOrThreadLocalScoped().Use<SettingService>();
            For<IDashboardService>().HybridHttpOrThreadLocalScoped().Use<DashboardService>();

            For<List<IDashboardWidgetDataCreator>>().HybridHttpOrThreadLocalScoped()
                .Use("", TypeHelper.FindListOfByContainer<IDashboardWidgetDataCreator>);

            For<IFileService>().HybridHttpOrThreadLocalScoped().Use("decorate for observers", c =>
            {
                var subject = c.GetInstance<FileServiceSubject>();
                subject.SetWrapee(c.GetInstance<FileService>());
                subject.AddObserver(c.GetInstance<FileDataImageCropperObserver>());
                return subject;
            });
            
        }
    }
}
