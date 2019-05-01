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
using VarinaCmsV2.Core.Logic.Widgets.ProductCategory;
using VarinaCmsV2.Core.Services.Product;

namespace VarinaCmsV2.Core.Logic.Widgets.Products
{
    public class ProductWidget  : BaseWidgetWithMetaData<ProductWidgetMetaData>
    {
        private readonly IProductDataService _productDataService;

        public ProductWidget( )
        {
            _productDataService = Container.GetInstance<IProductDataService>();
        }

        public override string Type { get; } = "product";

        public override BaseWidgetLiquidData Parse(JObject widgetMetaData)
        {
            DeserializeMetaData(widgetMetaData);
            var entities =
                _productDataService.Query
                    .Include(x => x.Tags)
                    .Include(x => x.Fields)
                    .Include(x => x.Fields.Select(f => f.FieldDefenition))
                    .Include(x => x.Creator)
                    .Include(x=>x.Pictures)
                    .Include(x => x.RelatedCategories)
                    .Include(x => x.PrimaryCategory)
                    .GetWithAppliedOrder(MetaData.OrderOption).GetWithApliedTimeRange(MetaData.TimeRange);

            return new ProductWidgetLiquidData() { Entities = entities.Take(MetaData.Count).ToList().AsLiquidAdapted() };
        }
    }
}
