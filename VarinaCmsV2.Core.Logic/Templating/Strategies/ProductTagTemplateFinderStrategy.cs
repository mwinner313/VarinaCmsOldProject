using VarinaCmsV2.Core.Contracts.Templating;
using VarinaCmsV2.DomainClasses.Contracts;

namespace VarinaCmsV2.Core.Logic.Templating.Strategies
{
    internal class ProductTagTemplateFinderStrategy : ILiquidTemplateFinderStrategy
    {
        public string GetTemplatePathDefault(ITemplatedItem item = null)
        {
            return "tag-product";
        }

        public string GetTemplatePathByConvention(ITemplatedItem item)
        {
           return $"Product/tag-product-{item.Handle}";
        }
    }
}