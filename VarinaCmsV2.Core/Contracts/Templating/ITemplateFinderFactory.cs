using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.Core.Infrastructure;
using VarinaCmsV2.DomainClasses.Contracts;

namespace VarinaCmsV2.Core.Contracts.Templating
{
    public interface ITemplateFinderFactory
    {
        string TemplatePath { get; set; }
        ILiquidTemplateFinderStrategy GetStrategy(ITemplatedItem item);
        ILiquidTemplateFinderStrategy GetHomePageStrategy(string basePath);
        ILiquidTemplateFinderStrategy Get404NotFoundStrategy(string basePath);
    }
}
