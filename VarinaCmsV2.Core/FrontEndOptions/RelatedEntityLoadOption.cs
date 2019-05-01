using VarinaCmsV2.Common;

namespace VarinaCmsV2.Core.FrontEndOptions
{
    public class RelatedEntityLoadOption
    {
        public OrderOption Order { get; set; }
        public TimeRangeOption TimeRange { get; set; }
        public int Count { get; set; }
        public string Handle { get; set; }
    }
}