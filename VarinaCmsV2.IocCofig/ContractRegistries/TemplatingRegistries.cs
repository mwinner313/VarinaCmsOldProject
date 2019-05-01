using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using StructureMap.Web;
using VarinaCmsV2.Configuration.Base;
using VarinaCmsV2.Core.Contracts.Configuration.Base;
using VarinaCmsV2.Core.Contracts.Templating;
using VarinaCmsV2.Core.Logic.Templating;
using VarinaCmsV2.Data.Contracts;
using StructureMap;

namespace VarinaCmsV2.IocCofig.ContractRegistries
{
    public class TemplatingRegistries:StructureMap.Registry
    {
        public TemplatingRegistries()
        {
            For<ITemplateFinderFactory>().HybridHttpOrThreadLocalScoped().Use("Configure Base Directory For Views",(c) =>
            {
                var basePath = WebConfigurationManager.AppSettings.Get("template-base-path");
                return new TemplateFinderFactory(basePath,c.GetInstance<IContainer>());
            });
            For<ITemplateProvider>().Singleton().Use<CacheTemplateProvider>();
        }
    }
}
