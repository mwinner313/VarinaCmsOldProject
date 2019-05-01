using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using PersianDate;
using VarinaCmsV2.Common;
using VarinaCmsV2.Core.Contracts.DataServices;

namespace VarinaCmsV2.Core.Logic.Services.Dashboard.WidgetDataCreators
{
    public class OrderChartDataCreator : IDashboardWidgetDataCreator
    {
        private readonly IOrderDataService _orderDataService;

        public OrderChartDataCreator(IOrderDataService orderDataService)
        {
            _orderDataService = orderDataService;
        }

        public string Name => "OrderChart";
        public object CreateData()
        {
            return new Dictionary<string, List<ChartData>>
            {
                {"سال", CreateForDateRange(DateHelper.GetStartOfYearFa(), "MMM")},
                {"ماه", CreateForDateRange(DateHelper.GetStartOfMonthFa(), " d ")},
                {"هفته", CreateForDateRange(DateHelper.GetStartOfWeekFa(), "ddd")},
                {"امروز", CreateForDateRange(DateHelper.GetStartOfDayFa(), "hh")}
            };
        }

        private List<ChartData> CreateForDateRange(DateTime startOfYear, string dateFormatAndRangeName)
        {
            return _orderDataService.Query.Where(x => x.CreateDateTime > startOfYear)
                .Select(x => x.CreateDateTime)
                .ToList()
                .Select(x => x.ToFa(dateFormatAndRangeName))
                .GroupBy(x => x)
                .Select(x => new ChartData { Range = x.Key.Trim(), Count = x.LongCount() })
                .ToList().FillEmptyEntiries(dateFormatAndRangeName);
        }
    }
}
