using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StructureMap;
using VarinaCmsV2.Common;
using VarinaCmsV2.Data.Infrastructure;

namespace VarinaCmsV2.IocCofig.ContractRegistries
{
   public class InfrastructureDataGlobalFilterRegisties:Registry
    {
        public InfrastructureDataGlobalFilterRegisties()
        {
            For<List<GlobalFilter>>().Singleton().Use("Registring globalFilters", TypeHelper.FindListOf<GlobalFilter>);
        }
        private  List<GlobalFilter> FindGlobalFilters(StructureMap.IContext c) 
        {
            var gbType = typeof(GlobalFilter);
            var filters = gbType.Assembly.GetTypes().Where(x => gbType.IsAssignableFrom(x) && gbType != x)
             .Select(Activator.CreateInstance)
             .Select(x => x as GlobalFilter)
             .ToList();
            filters.ForEach(c.BuildUp);
            return filters;
        }
    }
}
