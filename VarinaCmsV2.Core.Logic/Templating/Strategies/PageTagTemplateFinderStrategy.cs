using System;
using VarinaCmsV2.Core.Contracts.Templating;
using VarinaCmsV2.DomainClasses.Contracts;

namespace VarinaCmsV2.Core.Logic.Templating.Strategies
{
    public class PageTagTemplateFinderStrategy : ILiquidTemplateFinderStrategy
    {

        public PageTagTemplateFinderStrategy()
        {
        }

        public string GetTemplatePathDefault(ITemplatedItem item = null)
        {
            return "tag-page";
        }

        public string GetTemplatePathByConvention(ITemplatedItem item)
        {
            return $"Page/tag-page-{item.Handle}";
        }
    }
}
