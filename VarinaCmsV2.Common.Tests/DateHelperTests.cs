using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Humanizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PersianDate;

namespace VarinaCmsV2.Common.Tests
{
    [TestClass]
    public class DateHelperTests
    {
        [TestMethod]
        public void ShouldCreateCorrectDate()
        {
         Console.WriteLine(DateHelper.ParseSafe("1396/5/16 - 23:20"));   
        }

        [TestMethod]
        public void ShouldGlobalizeDate()
        {
            Console.WriteLine(DateTime.Now.ToString("dddd d MMMM yyy", new CultureInfo("en-US")));
            Console.WriteLine(DateTime.Now.ToString("dddd d MMMM yyy", new CultureInfo("fa-IR")));
            Console.WriteLine(DateTime.Now.ToString(" M ", new CultureInfo("fa-IR")));
            Console.WriteLine(DateTime.Now.ToString("hh", new CultureInfo("fa-IR")));
            Console.WriteLine(DateTime.Now.ToString("hh", new CultureInfo("es-CL")));
            Console.WriteLine(DateTime.Now.ToString("mm", new CultureInfo("es-CL")));
            Console.WriteLine(DateTime.Now.ToString(" d ", new CultureInfo("fa-IR")));
            Console.WriteLine(DateTime.Now.ToString("HH:mm:ss tt zz", new CultureInfo("fa-IR")));
            Console.WriteLine(DateTime.Now.ToString(" d ", new CultureInfo("en-US")));
            Console.WriteLine(DateTime.Now.ToUniversalTime().AddHours(-2).Humanize(culture:new CultureInfo("fa-IR")));
            Console.WriteLine(DateTime.Now.Ticks);
        }

        [TestMethod]
        public void ShouldGetFirstDayOfTheYearInPersianDate()
        {
            Console.Write(DateHelper.GetStartOfYearFa());
        }
        [TestMethod]
        public void ShouldGetStartOfWeekFa()
        {
            Console.Write(DateHelper.GetStartOfWeekFa().ToFa("YYYY/MM/d  hh:mm:ss"));
        }
        [TestMethod]
        public void ShouldGetStartOfDayFa()
        {
            Console.Write(DateHelper.GetStartOfDayFa().ToFa("YYYY/MM/d  hh:mm:ss"));
        }
        [TestMethod]
        public void ShouldGetStartOfDayMonth()
        {
            Console.Write(DateHelper.GetStartOfMonthFa().ToFa("YYYY/MM/d  hh:mm:ss"));
        }
        [TestMethod]
        public void ShowAllDate()
        {

            Console.WriteLine(DateHelper.GetStartOfDayFa().ToFa("YYYY/MM/d  hh:mm:ss"));
            Console.WriteLine(DateHelper.GetStartOfWeekFa().ToFa("YYYY/MM/d  hh:mm:ss"));
            Console.WriteLine(DateHelper.GetStartOfMonthFa().ToFa("YYYY/MM/d  hh:mm:ss"));
            Console.WriteLine(DateHelper.GetStartOfYearFa().ToFa("YYYY/MM/d  hh:mm:ss"));
            Console.WriteLine(PersianDateTime.GetMonthName(-1));
            Console.WriteLine(PersianDateTime.GetDaysInMonth(PersianDateTime.Now.Year, PersianDateTime.Now.Month ));
        }
    }
}
