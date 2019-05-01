using System.Data.Entity;
using System.Linq;
using DotLiquid;
using Newtonsoft.Json.Linq;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.Core.Logic.Helpers;
using VarinaCmsV2.Core.Contracts.Widget;

namespace VarinaCmsV2.Core.Logic.Widgets.Category
{
    public class CategoryWidget:BaseWidgetWithMetaData<CategoryWidgetMetaData>
    {
        private readonly ICategoryDataService _categoryDataService;
        private readonly IEntityDataService _entityDataService;
        public CategoryWidget()
        {
            _entityDataService = Container.GetInstance<IEntityDataService>();
            _categoryDataService = Container.GetInstance<ICategoryDataService>();
        }

        public override string Type { get; } = "category";
        public override BaseWidgetLiquidData Parse(JObject widgetMetaData)
        {
            base.DeserializeMetaData(widgetMetaData);
            var category = _categoryDataService.Query.Include(x=>x.Scheme).First(x => x.Id == MetaData.CategoryId);
            var entities =
                _entityDataService.Query.AsNoTracking().Where(
                        x =>
                            x.PrimaryCategoryId == MetaData.CategoryId ||
                            x.RelatedCategories.Any(r => r.Id == MetaData.CategoryId))
                    .Include(x => x.Creator)
                    .Include(x=>x.Scheme)
                    .Include(x => x.Tags)
                    .Include(x=>x.Fields)
                    .Include(x=>x.Fields.Select(f=>f.FieldDefenition))
                    .Include(x => x.PrimaryCategory)
                    .Include(x => x.PrimaryCategory.Scheme)
                    .Include(x => x.RelatedCategories)
                    .Include(x => x.RelatedCategories.Select(r=>r.Scheme))
                    .GetWithAppliedOrder(MetaData.OrderOption)
                    .GetWithApliedTimeRange(MetaData.TimeRange)
                    .Take(MetaData.EntitiesCount)
                    .ToList();
            return new CategoryWidgetLiquidData() { Category = category.AsLiquidAdaptedWithEntities(entities) };
        }
    }
}
