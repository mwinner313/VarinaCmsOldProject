using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using PersianDate;
using VarinaCmsV2.Common;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.Core.Security.Premissions;
using VarinaCmsV2.Security.Contracts;

namespace VarinaCmsV2.Core.Logic.Services.Dashboard.WidgetDataCreators
{
    class WebSiteUserRegisterChart : IDashboardWidgetDataCreator
    {
        private readonly IAppUserManager _userManager;
        public WebSiteUserRegisterChart(IAppUserManager userManager)
        {
            _userManager = userManager;
        }

        public string Name => nameof(WebSiteUserRegisterChart);
        public object CreateData()
        {
            var retVal = new Dictionary<string, List<ChartData>>
            {
                {"سال", CreateForDateRange(DateHelper.GetStartOfYearFa(), "MMM")},
                {"ماه", CreateForDateRange(DateHelper.GetStartOfMonthFa(), " d ")},
                {"هفته", CreateForDateRange(DateHelper.GetStartOfWeekFa(), "ddd")},
                {"امروز", CreateForDateRange(DateHelper.GetStartOfDayFa(), "hh")}
            };

            return retVal;
        }
        private List<ChartData> CreateForDateRange(DateTime startOfYear, string dateFormatAndRangeName)
        {
            var retVal = _userManager.GetUsersInRole(PreDefRoles.WebSiteUser).Where(x => x.CreateDateTime > startOfYear)
                .Select(x => x.CreateDateTime)
                .ToList()
                .Select(x => x.ToFa(dateFormatAndRangeName))
                .GroupBy(x => x)
                .Select(x => new ChartData { Range = x.Key, Count = x.LongCount() })
                .ToList();
            return retVal.FillEmptyEntiries(dateFormatAndRangeName);
        }

      
    }
}
