using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.Core.FrontEndOptions;
using VarinaCmsV2.Core.Logic.Helpers;
using VarinaCmsV2.DomainClasses.Entities.Enums;
using VarinaCmsV2.ViewModel.Menu;

namespace VarinaCmsV2.Core.Logic.Widgets.Menu
{
    public class CategoryMenuitemDataDecorator : MenuItemLiquidDecorator
    {
        private readonly ICategoryDataService _categories;
        private readonly IEntityDataService _entities;
        public CategoryMenuitemDataDecorator(ICategoryDataService dataservice, IEntityDataService entities)
        {
            _categories = dataservice;
            _entities = entities;
        }
        public CategoryMenuitemDataDecorator(MenuItemLiquidViewModel wrapee, ICategoryDataService dataservice, IEntityDataService entities) : base(wrapee)
        {
            _categories = dataservice;
            _entities = entities;
        }

        public override MenuItemLiquidViewModel Decorate(MenuItemLiquidViewModel wrappe)
        {
            return new CategoryMenuitemDataDecorator(wrappe, _categories,_entities);
        }
        public override object BeforeMethod(string method)
        {
            if (method != "target_category" || TargetType != nameof(MenuItemTargetType.Category))
                return base.BeforeMethod(method);

            var category = _categories
                .Query
                .Include(x => x.Scheme)
                .Include(x => x.Childs)
                .First(x => x.Id == TargetId);

            var categoryIds = GetChildRescursivelyCategoryIds(category);

            var entities = _entities
                .Query
                .IncludeNecessaryData()
                .Where(x => categoryIds.Contains(x.PrimaryCategoryId))
                .OrderByDescending(x => x.PublishDateTime)
                .Take(FrontEndDeveloperOptions.Instance.MegaMenuItemSize)
                .AsNoTracking()
                .ToList();

            return category.AsLiquidAdaptedWithEntities(entities);
        }

        private List<Guid> GetChildRescursivelyCategoryIds(DomainClasses.Entities.Category category)
        {
            var ids = new List<Guid>();
            if (category == null) return ids;
            ids.Add(category.Id);
            if (category.Childs != null)
                foreach (var ch in category.Childs)
                {
                    ids.AddRange(GetChildRescursivelyCategoryIds(ch));
                }
            return ids;
        }
    }
}
