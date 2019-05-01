using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.Core.Infrastructure;
using VarinaCmsV2.DomainClasses.Contracts;

namespace VarinaCmsV2.Core.Contracts.Templating
{
    public interface ILiquidTemplateFinderStrategy
    {
        string GetTemplatePathDefault(ITemplatedItem item = null);
        string GetTemplatePathByConvention(ITemplatedItem item = null);
    }
}
