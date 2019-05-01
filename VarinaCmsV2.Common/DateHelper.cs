using System;
using System.Collections.Generic;
using System.Globalization;
using PersianDate;

namespace VarinaCmsV2.Common
{
    public static class DateHelper
    {
        /// <summary>
        /// parse string date with format YYYY/MM/DD - hh:mm 
        /// if can not returns DateTime.MinValue
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime ParseSafe(string date)
        {
            if (date == null) return DateTime.MinValue;
            try
            {
                if (!date.Contains("-"))
                    return ParsDateOnly(date);
                return ParseDateTime(date);
            }
            catch (Exception)
            {
                return DateTime.MinValue;
            }
        }

        private static DateTime ParseDateTime(string datetime)
        {
            var dateAndTime = datetime.Split('-');
            var date = ParsDateOnly(dateAndTime[0]);
            var time = dateAndTime[1].Split(':');

            if (date == DateTime.MinValue || time.Length != 2) return DateTime.MinValue;

            var hour = Convert.ToInt32(time[0]);
            var minutes = Convert.ToInt32(time[1]);
            if (minutes >= 60 || hour >= 24) return DateTime.MinValue;
            return date.AddHours(hour).AddMinutes(minutes);

        }

        private static DateTime ParsDateOnly(string date)
        {
            var dp = date.Split('/');
            if (dp.Length != 3) return DateTime.MinValue;

            return new DateTime(Convert.ToInt32(dp[0]),
                Convert.ToInt32(dp[1]), Convert.ToInt32(dp[2]), new PersianCalendar());

        }

        public static DateTime GetStartOfWeekEn()
        {
            var now = DateTime.Now;
            var offSet = Convert.ToDouble(now.DayOfWeek);
            var startOfWeek = now.Subtract(TimeSpan.FromDays(offSet + 1));
            startOfWeek = startOfWeek.AddHours(-startOfWeek.Hour);
            startOfWeek = startOfWeek.AddMinutes(-startOfWeek.Minute);
            startOfWeek = startOfWeek.AddSeconds(-startOfWeek.Second);
            return startOfWeek;
        }

        public static DateTime GetStartOfWeekFa()
        {
            var now = DateTime.Now;
            var offSet = Convert.ToDouble(now.DayOfWeek);
            var startOfWeek = now.Subtract(TimeSpan.FromDays(offSet + 1));
            startOfWeek = startOfWeek.AddHours(-Convert.ToInt32(startOfWeek.ToFa("hh")));
            startOfWeek = startOfWeek.AddMinutes(-Convert.ToInt32(startOfWeek.ToFa("mm")));
            startOfWeek = startOfWeek.AddSeconds(-Convert.ToInt32(startOfWeek.ToFa("ss")));
           return  startOfWeek;
        }
        public static DateTime GetStartOfDayEn()
        {
            var now = DateTime.Now;
            now = now.AddHours(-now.Hour);
            now = now.AddMinutes(-now.Minute);
            now = now.AddSeconds(-now.Second);
            return now;
        }
        public static DateTime GetStartOfDayFa()
        {
            var now = DateTime.Now;
            now = now.AddHours(-Convert.ToInt32(now.ToFa("hh")));
            now = now.AddMinutes(-Convert.ToInt32(now.ToFa("mm")));
            now = now.AddSeconds(-Convert.ToInt32(now.ToFa("ss")));
            return now;
        }


        public static DateTime GetEndOfWeek()
        {
            var now = DateTime.Now;
            var offSet = Convert.ToDouble(now.DayOfWeek);
            var endOfWeek = now.AddDays(7 - offSet);
            endOfWeek = endOfWeek.AddHours((23 - endOfWeek.Hour));
            endOfWeek = endOfWeek.AddMinutes(59 - endOfWeek.Minute);
            endOfWeek = endOfWeek.AddSeconds(59 - endOfWeek.Second);
            return endOfWeek;
        }
        public static DateTime GetStartOfMonthEn()
        {
            var now = DateTime.Now;
            return new DateTime(now.Year, now.Month, 1);
        }
        public static DateTime GetStartOfMonthFa()
        {
            return GetStartOfYearFa().AddMonths(Convert.ToInt32(DateTime.Now.ToFa("MM"))-1);
        }
        public static DateTime GetEndOfMonth()
        {
            var now = DateTime.Now;
            return new DateTime(now.Year, now.Month, DateTime.DaysInMonth(now.Year, now.Month));
        }
        public static DateTime GetStartOfYearFa()
        {
            var currentYear = Convert.ToInt32(DateTime.Now.ToFa("YYYY"));
            return new PersianCalendar().ToDateTime(currentYear,1,1,0,0,0,0);
        }

        public static Dictionary<string, int> WeekDayNames => new Dictionary<string, int>()
        {
            ["شنبه"] = 1,
            ["یک شنبه"] = 2,
            ["دو شنبه"] = 3,
            ["سه شنبه"] = 4,
            ["چهار شنبه"] = 5,
            ["پنج شنبه"] = 6,
            ["جمعه"] = 7,
        };
    }
}
