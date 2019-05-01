using System;
using VarinaCmsV2.Common;

namespace VarinaCmsV2.Core.Logic.Widgets.ProductCategory
{
    public class ProductCategoryWidgetMetaData
    {
        public Guid CategoryId { get; set; }
        public int ProductCount { get; set; }
        public TimeRangeOption TimeRange { get; set; }
        public OrderOption OrderOption { get; set; }
    }
}