using VarinaCmsV2.Core.Contracts.Templating;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.ViewModel.Entities;

namespace VarinaCmsV2.Core.Logic.Templating.Strategies
{
    internal class EntitySearchTemplateFinderStrategy : ILiquidTemplateFinderStrategy
    {


        public EntitySearchTemplateFinderStrategy()
        {
        }

        public string GetTemplatePathDefault(ITemplatedItem item = null)
        {
            var searchResult = item as EntitySearchResaultLiquidAdapted;
            return $"search-{searchResult.Scheme.Handle}";
        }

        public string GetTemplatePathByConvention(ITemplatedItem item = null)
        {
            var searchResult = item as EntitySearchResaultLiquidAdapted;
            return $"search-{searchResult.Scheme.Handle}";
        }
    }
}