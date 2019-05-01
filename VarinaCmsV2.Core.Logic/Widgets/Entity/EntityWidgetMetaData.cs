using System;
using VarinaCmsV2.Common;

namespace VarinaCmsV2.Core.Logic.Widgets.Entity
{
    public class EntityWidgetMetaData
    {
        public Guid SchemeId { get; set; }
        public int Count { get; set; }
        public TimeRangeOption TimeRange { get; set; }
        public OrderOption OrderOption { get; set; }
    }
}