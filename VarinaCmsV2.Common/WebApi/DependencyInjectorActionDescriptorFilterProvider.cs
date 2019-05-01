using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using StructureMap;

namespace VarinaCmsV2.Common.WebApi
{
    public class DependencyInjectorActionDescriptorFilterProvider: ActionDescriptorFilterProvider, IFilterProvider
    {
        private readonly IContainer _container;
        public DependencyInjectorActionDescriptorFilterProvider(IContainer container)
        {
            _container = container;
        }
      
        public new IEnumerable<FilterInfo> GetFilters(HttpConfiguration configuration, HttpActionDescriptor actionDescriptor)
        {
            var filters = base.GetFilters(configuration, actionDescriptor);

            var filterInfos = filters as FilterInfo[] ?? filters.ToArray();
            foreach (var filter in filterInfos)
            {
                _container.BuildUp(filter.Instance);
            }

            return filterInfos;
        }
    }
}
