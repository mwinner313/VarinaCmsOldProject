using System.Web;
using System.Web.Configuration;
using System.Web.Routing;
using VarinaCmsV2.Core.Contracts.DataServices;
using Microsoft.AspNet.Identity.Owin;
using VarinaCmsV2.Common;
using System.Data.Entity;
using System;
using System.Linq;

namespace VarinaCmsV2.Web.RouteConstraints
{
    internal class EntityDateRouteConstraint : IRouteConstraint
    {
        private IEntityDataService _entities;

        private const string Year = "year";
        private const string Day = "day";
        private const string Month = "month";
        private const string EntityUrl = "entityUrl";
        private const string SchemeUrl = "schemeUrl";

        private string SchemeUrlValue { get; set; }
        private string LangValue { get; set; } = WebConfigurationManager.AppSettings["default-language"];
        private string EntityUrlValue { get; set; }
        private int DayValue { get; set; }
        private int MonthValue { get; set; }
        private int YearValue { get; set; }
        public EntityDateRouteConstraint()
        {
        }
        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values,
         RouteDirection routeDirection)
        {
            _entities = Startup.Container.GetInstance<IEntityDataService>();
            var languageDataService = Startup.Container.GetInstance<ILanguageDataService>();

            if (TryGetRequiredRouteValues(values))
            {
                if (languageDataService.FromCache().Any(x => x.Name == LangValue))
                    httpContext.Items.AddIfNotExist("lang", LangValue);
                return ExistsByConstraint();
            }
            return false;
        }


        private bool ExistsByConstraint()
        {
            var routeDate = DateTime.MinValue;

            try
            {
                routeDate = new DateTime(YearValue, MonthValue, DayValue);
            }
            catch (Exception)
            {
                return false;
            }
            return
                AsyncHelper.RunSync(() => _entities.Query.AnyAsync(x =>
                            x.Url == EntityUrlValue &&
                            x.Scheme.Url == SchemeUrlValue &&
                            x.LanguageName == LangValue &&
                            x.CreateDateTime.Date == routeDate));


        }
        private bool TryGetRequiredRouteValues(RouteValueDictionary values)
        {
            object entityValue;
            object handleUrlValue;
            object dayValue;
            object monthValue;
            object yearValue;
            if (values.TryGetValue(SchemeUrl, out entityValue))
            {
                SchemeUrlValue = entityValue as string;
            }

            if (values.TryGetValue(EntityUrl, out handleUrlValue))
            {
                EntityUrlValue = handleUrlValue as string;
            }


            if (values.TryGetValue(Day, out dayValue))
            {
                int _dayValue = 0;
                if (int.TryParse(dayValue.ToString(), out _dayValue))
                {
                    MonthValue = _dayValue;
                }
            }
            if (values.TryGetValue(Year, out yearValue))
            {
                int _yearValue = 0;
                if (int.TryParse(yearValue.ToString(), out _yearValue))
                {
                    YearValue = _yearValue;
                }
            }
            if (values.TryGetValue(Month, out monthValue))
            {
                int _monthValue = 0;
                if (int.TryParse(monthValue.ToString(), out _monthValue))
                {
                    MonthValue = _monthValue;
                }
            }
            return !IsAnyPropertyNull();
        }

        private bool IsAnyPropertyNull()
        {
            return string.IsNullOrEmpty(EntityUrlValue)
                   || string.IsNullOrEmpty(SchemeUrlValue)
                   || string.IsNullOrEmpty(LangValue)
                   || MonthValue == 0
                   || YearValue == 0
                   || DayValue== 0
                 ;
        }


    }
}