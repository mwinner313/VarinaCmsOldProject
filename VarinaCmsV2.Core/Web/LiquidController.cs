using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using System.Web.Configuration;
using System.Web.Mvc;
using DotLiquid;
using DotLiquid.FileSystems;
using VarinaCmsV2.Common;
using VarinaCmsV2.Common.MVC;
using VarinaCmsV2.Core.Contracts.Decorators;
using VarinaCmsV2.Core.Contracts.Templating;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.ViewModel;

namespace VarinaCmsV2.Core.Web
{
    public class LiquidController : BaseController
    {
        public const string LiquidDefaulViewPath = "~/Views/LiquidDefaulView.cshtml";
        protected string ViewTemplatesBasePath { private set; get; }
        protected ITemplateProvider LiquidTemplateProvider { private set; get; }

        public LiquidController()
        {
            ConfigureDependencies();
            ConfigureViewsBasePath();
        }

        private void ConfigureDependencies()
        {
            LiquidTemplateProvider = Resolver.GetService(typeof(ITemplateProvider)).Cast<ITemplateProvider>();
        }

        private void ConfigureViewsBasePath()
        {
            ViewTemplatesBasePath = AppDomain.CurrentDomain.BaseDirectory + WebConfigurationManager.AppSettings["template-base-path"];
            LiquidTemplateProvider.TemplatesBasePath = ViewTemplatesBasePath;
            LiquidTemplateProvider.Extension = WebConfigurationManager.AppSettings["template-extension"];
            Template.FileSystem = new LocalFileSystem(Path.GetDirectoryName(ViewTemplatesBasePath));
        }

        protected Drop GetDecoratedLiquidViewData(LiquidAdapter drop = null)
        {
            foreach (var decorator in ((List<LiquidDataDecorator>)Resolver.GetService(typeof(List<LiquidDataDecorator>))).OrderBy(x => x.LevelToReachRealWrappe).ToList())
            {
                if (decorator.ShouldDecorate(drop))
                    drop = decorator.Decorate(drop);
            }
            return drop;
        }

        protected ActionResult LiquidView<T>(T model) where T : LiquidAdapter
        {
            var template = GetParsedTemplate(model);
            var viewData = GetDecoratedLiquidViewData(model);
            return View(LiquidDefaulViewPath, new LiquidTemplateViewModel()
            {
                RenderedTemplate = template.Render(Hash.FromAnonymousObject(new
                {
                    vm = viewData
                }))
            });
        }

        protected ActionResult LiquidView<T>(T model, string templateName) where T : LiquidAdapter
        {
            var template = GetParsedTemplate(templateName);
            var viewData = GetDecoratedLiquidViewData(model);
            return View(LiquidDefaulViewPath, new LiquidTemplateViewModel()
            {
                RenderedTemplate = template.Render(Hash.FromAnonymousObject(new
                {
                    vm = viewData
                }))
            });
        }



        protected ActionResult LiquidNotFoundView()
        {
            return View(LiquidDefaulViewPath, new LiquidTemplateViewModel()
            {
                RenderedTemplate = Template.Parse(LiquidTemplateProvider.Get404NotFoundTemplateString()).Render(Hash.FromAnonymousObject(new
                {
                    vm = GetDecoratedLiquidViewData()
                }))
            });
        }
        protected ActionResult LiquidHomePageView(HomePageDataLiquidViewModel model)
        {
            return View(LiquidDefaulViewPath, new LiquidTemplateViewModel()
            {
                RenderedTemplate = Template.Parse(LiquidTemplateProvider.GetHomePageTemplateString()).Render(Hash.FromAnonymousObject(new
                {
                    vm = GetDecoratedLiquidViewData(model)
                }))
            });
        }

        private Template GetParsedTemplate(ITemplatedItem model)
        {
            return Template.Parse(LiquidTemplateProvider.GetTemplateString(model));
        }
        private Template GetParsedTemplate(string templateName)
        {
            return Template.Parse(System.IO.File.ReadAllText(LiquidTemplateProvider.TemplatesBasePath + templateName + LiquidTemplateProvider.Extension));
        }
    }
}
