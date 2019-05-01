using StructureMap.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.Core.Contracts.WebClientServices;
using VarinaCmsV2.Core.Contracts.WebClientServices.Cart;
using VarinaCmsV2.Core.Contracts.WebClientServices.Comment;
using VarinaCmsV2.Core.Contracts.WebClientServices.Discounts;
using VarinaCmsV2.Core.Contracts.WebClientServices.Entities;
using VarinaCmsV2.Core.Contracts.WebClientServices.Orders;
using VarinaCmsV2.Core.Contracts.WebClientServices.Pages;
using VarinaCmsV2.Core.Contracts.WebClientServices.Products;
using VarinaCmsV2.Core.Logic.Services.Cart;
using VarinaCmsV2.Core.Logic.WebClientServiceObservers;
using VarinaCmsV2.Services.DataServices;
using VarinaCmsV2.Services.WebClientObservers.Orders;
using VarinaCmsV2.Services.WebClientServices;

namespace VarinaCmsV2.IocCofig.ContractRegistries
{
    public class WebClientServiceRegistries : StructureMap.Registry
    {
        public WebClientServiceRegistries()
        {
            For<List<EntityWebClientServiceObserver>>().HybridHttpOrThreadLocalScoped().Use("registring observers for EntityWebClientService", c =>
                 new List<EntityWebClientServiceObserver>() { c.GetInstance<EntityVisitNumberobserver>() }
            );

            For<IEntityWebClientService>().HybridHttpOrThreadLocalScoped().Use(" registring EntityWebClientServiceSubject for IEntityWebClientService",
                c => new EntityWebClientServiceSubject(c.GetInstance<EntityWebClientService>(), c.GetInstance<List<EntityWebClientServiceObserver>>()));

            For<IEntitySchemeWebClientService>().HybridHttpOrThreadLocalScoped().Use<EntitySchemeWebClientService>();

            For<IPageWebClientService>().HybridHttpOrThreadLocalScoped().Use<PageWebClientService>();
            For<IProductWebClientService>().HybridHttpOrThreadLocalScoped().Use<ProductWebClientService>();
            For<IShoppingCartHelper>().HybridHttpOrThreadLocalScoped().Use<ShoppingCartHelper>();
            For<IDiscountWebClientService>().HybridHttpOrThreadLocalScoped().Use<DiscountWebClientService>();
            For<IOrderWebClientService>().HybridHttpOrThreadLocalScoped().Use("", c =>
            {
                var subject = c.GetInstance<OrderWebClientServiceSubject>();
                subject.SetWrappe(c.GetInstance<OrderWebClientService>());
                subject.AddObserver(c.GetInstance<WebSiteOwnerOrderNotifier>());
                return subject;
            });
            For<IDiscountValidator>().HybridHttpOrThreadLocalScoped().Use<DiscountValidator>();
            For<ICartService>().HybridHttpOrThreadLocalScoped().Use<CartService>();
            For<ICommentWebClientService>().HybridHttpOrThreadLocalScoped().Use("", c =>
            {
                var subject = c.GetInstance<CommentWebClientServiceSubject>();
                subject.SetWrappe(c.GetInstance<CommentWebClientService>());
                return subject;
            });
        }
    }
}
