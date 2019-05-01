using System.IO;
using VarinaCmsV2.Core.Contracts.Templating;
using VarinaCmsV2.DomainClasses.Contracts;

namespace VarinaCmsV2.Core.Logic.Templating.Strategies
{
    public class HomePageTemplateFinderStrategy: ILiquidTemplateFinderStrategy
    {
        public string GetTemplatePathDefault(ITemplatedItem item = null)
        {
            return "index";
        }

        public string GetTemplatePathByConvention(ITemplatedItem item = null)
        {
            return "index";
        }
    }
}
