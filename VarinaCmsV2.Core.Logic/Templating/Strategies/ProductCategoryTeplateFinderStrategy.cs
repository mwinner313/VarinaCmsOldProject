using VarinaCmsV2.Core.Contracts.Templating;
using VarinaCmsV2.DomainClasses.Contracts;

namespace VarinaCmsV2.Core.Logic.Templating.Strategies
{
    internal class ProductCategoryTeplateFinderStrategy : ILiquidTemplateFinderStrategy
    {
        public string GetTemplatePathDefault(ITemplatedItem item = null)
        {
            return "product-category";
        }

        public string GetTemplatePathByConvention(ITemplatedItem item = null)
        {
            return $"product-category-{item.Handle}";
        }
    }
}