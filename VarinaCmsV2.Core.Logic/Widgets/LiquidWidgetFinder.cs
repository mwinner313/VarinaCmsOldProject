using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotLiquid;
using StructureMap;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.Core.Contracts.Decorators;
using VarinaCmsV2.Core.Contracts.Templating;
using VarinaCmsV2.Core.Contracts.Widget;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.DomainClasses.Entities.Widgets;

namespace VarinaCmsV2.Core.Logic.Widgets
{
    public class LiquidWidgetFinder : Drop
    {
        private readonly List<LiquidDataDecorator> _dataDecorators;
        private readonly IWidgetDataService _widgetDataService;
        private readonly ITemplateProvider _templateProvider;
        private readonly IWidgetFactory _widgetFactory;
        public static Func<IContainer> GetContainer { get; set; }

        public LiquidWidgetFinder()
        {
            var container = GetContainer();
            _templateProvider = container.GetInstance<ITemplateProvider>();
            _widgetFactory = container.GetInstance<IWidgetFactory>();
            _dataDecorators = container.GetInstance<List<LiquidDataDecorator>>().OrderBy(x => x.LevelToReachRealWrappe)
                .ToList();
            _widgetDataService = container.GetInstance<IWidgetDataService>();
        }

        public override object BeforeMethod(string method)
        {
            var widgets = FindByContainerHandle(method);
            var html = Render(widgets);
            return html;
        }

        private string Render(List<Widget> widgets)
        {

            StringBuilder sb = new StringBuilder();
            foreach (var widget in widgets)
            {
                if (widget.Type == "html")
                {
                    RenderWithoutFindingTemplate(sb, widget);
                    continue;
                }

                var widgetLiquidData = _widgetFactory.Get(widget.Type).Parse(widget.MetaData);
                widgetLiquidData.Title = widget.Title;
                LiquidAdapter data = widgetLiquidData;
                _dataDecorators.ForEach(x => { data = x.Decorate(data); });
                var template = Template.Parse(_templateProvider.GetTemplateString(widget));
                var renderedWidget = template.Render(Hash.FromAnonymousObject(new { vm = data }));
                sb.Append(renderedWidget);
            }
            return sb.ToString();
        }

        private void RenderWithoutFindingTemplate(StringBuilder sb, Widget widget)
        {
            var emptyDrop = new LiquidAdapter();

            sb.Append(Template.Parse(widget.MetaData.ToObject<StaticHtml.StaticHtml>().HtmlContent)
                .Render(Hash.FromAnonymousObject(emptyDrop)));
            return;
        }

        private List<Widget> FindByContainerHandle(string method)
        {
            return _widgetDataService.Query.Where(x => x.Container.Handle == method).OrderBy(x => x.Order).ToList();
        }
    }
}