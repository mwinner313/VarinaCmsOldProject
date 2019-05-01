using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Routing;
using Microsoft.AspNet.Identity.Owin;
using VarinaCmsV2.Common;
using VarinaCmsV2.Core.Contracts.DataServices;

namespace VarinaCmsV2.Web.RouteConstraints
{
    public class EntityRouteConstraint : IRouteConstraint
    {
        private  IEntityDataService _entities;
        private ILanguageDataService _languageDataService;
        public EntityRouteConstraint()
        {
        }
        private const string SchemeUrl = "schemeUrl";
        private const string Lang = "lang";
        private const string EntityUrl = "entityUrl";
        private string SchemeUrlValue { get; set; }
        private string LangValue { get; set; } = WebConfigurationManager.AppSettings["default-language"];
        private string EntityUrlValue { get; set; }

        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values,
            RouteDirection routeDirection)
        {
            _entities = Startup.Container.GetInstance<IEntityDataService>();
            _languageDataService = Startup.Container.GetInstance<ILanguageDataService>();
            
            if (TryGetRequiredRouteValues(values))
            {
                if(_languageDataService.FromCache().Any(x=>x.Name==LangValue))
                httpContext.Items.AddIfNotExist("lang", LangValue);
                return ExistsEntityInSpecifiedLanguage();
            }
            return false;
        }

        private bool ExistsEntityInSpecifiedLanguage()
        {
            return
               _entities.Query.Any(x =>
                            x.Url == EntityUrlValue &&
                            x.Scheme.Url == SchemeUrlValue &&
                            x.Language.Name == LangValue);
        }
        private bool TryGetRequiredRouteValues(RouteValueDictionary values)
        {
            object entityValue;
            object handleUrlValue;
            object langValue;

            if (values.TryGetValue(SchemeUrl, out entityValue))
            {
                SchemeUrlValue = entityValue as string;
            }

            if (values.TryGetValue(Lang, out langValue))
            {
                LangValue = langValue as string;
            }

            if (values.TryGetValue(EntityUrl, out handleUrlValue))
            {
                EntityUrlValue = handleUrlValue as string;
            }
            return !IsAnyPropertyNull();
        }

        private bool IsAnyPropertyNull()
        {
            return EntityUrlValue == null
                   ||SchemeUrlValue == null
                 ;
        }
    }
}