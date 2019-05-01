using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.Core.Contracts.Widget;
using VarinaCmsV2.Core.Logic.Helpers;

namespace VarinaCmsV2.Core.Logic.Widgets.ProductCategory
{
    public class ProductCategoryWidget : BaseWidgetWithMetaData<ProductCategoryWidgetMetaData>
    {
        public override string Type { get; } = "productCategory";
        private readonly IProductCategoryDataService _categoryDataService;
        private readonly IProductDataService _productDataService;

        public ProductCategoryWidget()
        {
            _productDataService = Container.GetInstance<IProductDataService>();
            _categoryDataService = Container.GetInstance<IProductCategoryDataService>();
        }

        public override BaseWidgetLiquidData Parse(JObject widgetMetaData)
        {
            base.DeserializeMetaData(widgetMetaData);
            var category = _categoryDataService.Query.First(x => x.Id == MetaData.CategoryId);
            category.Products =
                _productDataService.Query.AsNoTracking().Where(
                        x =>
                            x.PrimaryCategoryId == MetaData.CategoryId ||
                            x.RelatedCategories.Any(r => r.Id == MetaData.CategoryId))
                    .Include(x => x.Creator)
                    .Include(x => x.Tags)
                    .Include(x => x.Fields)
                    .Include(x => x.Fields.Select(f => f.FieldDefenition))
                    .Include(x => x.PrimaryCategory)
                    .Include(x => x.RelatedCategories)
                    .Include(x => x.RelatedCategories.Select(r => r.Scheme))
                    .GetWithAppliedOrder(MetaData.OrderOption)
                    .GetWithApliedTimeRange(MetaData.TimeRange)
                    .Take(MetaData.ProductCount)
                    .ToList();
            return new ProductCategoryWidgetLiquidData() { ProductCategory = category.ToLiquidViewModel() };
        }
    }
}
