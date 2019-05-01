using StructureMap;
using System;
using VarinaCmsV2.Core.Contracts.Templating;
using VarinaCmsV2.Core.Logic.Templating.Strategies;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.DomainClasses.Entities.Widgets;
using VarinaCmsV2.ViewModel.Category;
using VarinaCmsV2.ViewModel.DTO;
using VarinaCmsV2.ViewModel.Entities;
using VarinaCmsV2.ViewModel.Eshop;
using VarinaCmsV2.ViewModel.Eshop.Orders;
using VarinaCmsV2.ViewModel.Page;
using VarinaCmsV2.ViewModel.Tag;
using VarinaCmsV2.ViewModel.User;
using VarinaCmsV2.ViewModel.User.Account;
using VarinaCmsV2.ViewModel.Widget;

namespace VarinaCmsV2.Core.Logic.Templating
{
    public class TemplateFinderFactory : ITemplateFinderFactory
    {

        private readonly IContainer _container;
        public TemplateFinderFactory(string basePath, IContainer container)
        {
            if (string.IsNullOrEmpty(basePath))
                throw new NullReferenceException(nameof(basePath));
            TemplatePath = basePath;
            _container = container;
        }
        public ILiquidTemplateFinderStrategy GetStrategy(ITemplatedItem item = null)
        {
            if (item is UserLiquidViewModel)
                return new UserProfileTemplateFinderStrategy();

            if (item is PageLiquidViewModel)
                return new PageTemplateFinderStrategy();

            if (item is EntityLiquidAdapter)
                return new EntityTemplateFinderStrategy(_container);

            if (item is WidgetLiquidViewModel || item is Widget)
                return new WidgetTemplateFinderStrategy();

            if (item is EntityTagLiquidViewModel)
                return new EntityTagTemplateFinderStrategy();

            if (item is PageTagLiquidViewModel)
                return new PageTagTemplateFinderStrategy();

            if (item is ProductTagLiquidViewModel)
                return new ProductTagTemplateFinderStrategy();

            if (item is CategoryLiquidViewModel)
                return new CategoryTemplateFinderStrategy();

            if (item is EntitySearchResaultLiquidAdapted)
                return new EntitySearchTemplateFinderStrategy();

            if (item is UserEntitiesPaginatedLiquidAdapted)
                return new UserCreatedEntityTemplateFinderStrategy();
            if (item is ProductLiquidAdapter)
                return new ProductTeplateFinderStrategy();

            if (item is ProductCategoryLiquidVeiwModel)
                return new ProductCategoryTeplateFinderStrategy();

            if (item is UserCartLiquidViewModel)
                return new UserCartTeplateFinderStrategy();

            if (item is OrderLiquidViewModel)
                return new OrderTemplateFinderStrategy();

            return null;
        }

        public string TemplatePath { get; set; }
        public ILiquidTemplateFinderStrategy GetHomePageStrategy(string basePath)
        {
            return new HomePageTemplateFinderStrategy();
        }

        public ILiquidTemplateFinderStrategy Get404NotFoundStrategy(string basePath)
        {
            return new NotFound404TemplateFinderStrategy();
        }
    }
}
