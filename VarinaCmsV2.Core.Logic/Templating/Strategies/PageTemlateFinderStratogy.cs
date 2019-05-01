using VarinaCmsV2.Core.Contracts.Templating;
using VarinaCmsV2.DomainClasses.Contracts;

namespace VarinaCmsV2.Core.Logic.Templating.Strategies
{
    public class PageTemplateFinderStrategy : ILiquidTemplateFinderStrategy
    {
        public PageTemplateFinderStrategy()
        {
        }
        public string GetTemplatePathDefault(ITemplatedItem item)
        {

            return  $"page";
        }

        public string GetTemplatePathByConvention(ITemplatedItem item )
        {
           return $"Page/{item.Handle}";
        }
    }
}
