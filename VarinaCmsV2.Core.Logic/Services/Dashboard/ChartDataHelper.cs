using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.Common;

namespace VarinaCmsV2.Core.Logic.Services.Dashboard
{
    public static class ChartDataHelper
    {
        public static List<ChartData> FillEmptyEntiries(this List<ChartData> retVal, string dateFormatAndRangeName)
        {
            if (dateFormatAndRangeName == "MMM")
                return retVal.FillEmptyFaMonthes().OrderBy(x => x.Order).ToList();
            if (dateFormatAndRangeName == " d ")
                return retVal.FillEmptyFaDayOfMonth().OrderBy(x => x.Order).ToList();
            if (dateFormatAndRangeName == "ddd")
                return retVal.FillEmptyFaWeekDays().OrderBy(x => x.Order).ToList();
            if (dateFormatAndRangeName == "hh")
                return retVal.FillEmptyFaHours().OrderBy(x => x.Order).ToList();
            return retVal.OrderBy(x=>x.Order).ToList();
        }

        private static List<ChartData> FillEmptyFaHours(this List<ChartData> retVal)
        {
            for (int i = 0; i < 24; i++)
            {
                if (retVal.All(x => x.Range.Trim() != i.ToString())) retVal.Add(new ChartData() { Count = 0, Range = i.ToString() ,Order = i});
                else
                {
                    retVal.First(x => x.Range.Trim() == i.ToString()).Order = i;
                }
            }
            return retVal;

        }

        private static List<ChartData> FillEmptyFaWeekDays(this List<ChartData> retVal)
        {
            foreach (var dayName in DateHelper.WeekDayNames.Keys)
            {
                if (retVal.All(x => x.Range != dayName)) retVal.Add(new ChartData() { Count = 0, Range = dayName, Order = DateHelper.WeekDayNames[dayName] });
                else
                {
                    retVal.First(x => x.Range == dayName).Order = DateHelper.WeekDayNames[dayName];
                }
            }
            return retVal;
        }

        private static List<ChartData> FillEmptyFaDayOfMonth(this List<ChartData> retVal)
        {
            for (int i = 1; i <= PersianDateTime.GetDaysInMonth(PersianDateTime.Now.Year, PersianDateTime.Now.Month ); i++)
            {
                if (retVal.All(x => x.Range.Trim() != i.ToString())) retVal.Add(new ChartData() { Count = 0, Range = i.ToString(), Order = i });
                else
                {
                    retVal.First(x => x.Range.Trim() == i.ToString()).Order = i;
                }
            }
            return retVal;
        }

        private static List<ChartData> FillEmptyFaMonthes(this List<ChartData> retVal)
        {
            for (var i = -1; i <= 10; i++)
            {
                var monthName = PersianDateTime.GetMonthName(i);
                if (retVal.All(x => x.Range != monthName)) retVal.Add(new ChartData() { Count = 0, Range = monthName, Order = i });
                else
                {
                    retVal.First(x => x.Range == monthName).Order = i;
                }
            }
            return retVal;
        }
    }
}
