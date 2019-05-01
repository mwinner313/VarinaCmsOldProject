using System;
using System.IO;
using VarinaCmsV2.Core.Contracts.Templating;
using VarinaCmsV2.Core.Infrastructure;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.DomainClasses.Entities.Widgets;

namespace VarinaCmsV2.Core.Logic.Templating.Strategies
{
    public class WidgetTemplateFinderStrategy : ILiquidTemplateFinderStrategy
    {
        public WidgetTemplateFinderStrategy()
        {
        }


        public string GetTemplatePathDefault(ITemplatedItem item)
        {
            if (!(item is Widget widget))
                throw new NullReferenceException("invalid or null Entity passed for finding template");

            return GetTemplate(widget);
        }

        public string GetTemplatePathByConvention(ITemplatedItem item )
        {
            if (!(item is Widget widget))
                throw new NullReferenceException("invalid or null Entity passed for finding template");

            return $"Widgets/widget-{widget.Handle}";
        }

        private string GetTemplate(Widget widget)
        {
            return $"Widgets/widget-default-{widget.Type}";
        }
    }
}