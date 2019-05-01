using DotLiquid;
using PersianDate;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Humanizer;

namespace VarinaCmsV2.Common
{
    public class LiquidDateTime : Drop
    {
        private readonly DateTime _dateTime;
        public LiquidDateTime(DateTime dateTime)
        {
            _dateTime = dateTime;
        }

        public override object BeforeMethod(string method)
        {
            switch (method)
            {
                case "day": return ConvertDate.ToFa(_dateTime, " d ");
                case "hour": return ConvertDate.ToFa(_dateTime, "hh");
                case "minute": return ConvertDate.ToFa(_dateTime, "mm");
                case "year": return ConvertDate.ToFa(_dateTime, "yyyy");
                case "month": return ConvertDate.ToFa(_dateTime, "MM");
                case "week_day_name": return _dateTime.ToString("dddd", Thread.CurrentThread.CurrentCulture);
                case "month_name": return ConvertDate.ToFa(_dateTime, " d ");
                case "full": return _dateTime.ToString("dddd dd MMMM yyy");
                case "from_now": return _dateTime.ToUniversalTime().Humanize(culture: Thread.CurrentThread.CurrentCulture);
            }

            return base.BeforeMethod(method);
        }
    }
}
