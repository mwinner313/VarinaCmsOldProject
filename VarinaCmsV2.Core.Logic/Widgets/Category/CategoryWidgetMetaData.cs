using System;
using Newtonsoft.Json;
using VarinaCmsV2.Common;

namespace VarinaCmsV2.Core.Logic.Widgets.Category
{
    public class CategoryWidgetMetaData
    {
        
        public Guid CategoryId { get; set; }
        public int EntitiesCount { get; set; }
        public TimeRangeOption TimeRange { get; set; }
        public OrderOption OrderOption { get; set; }
    }
}