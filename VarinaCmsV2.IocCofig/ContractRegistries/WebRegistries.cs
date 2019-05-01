using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.Core.Contracts.Web;
using VarinaCmsV2.Core.Logic.Web.UrlBuilding;
using VarinaCmsV2.Core.Web.ActionFilters;

namespace VarinaCmsV2.IocCofig.ContractRegistries
{
    public class WebRegistries:StructureMap.Registry
    {
        public WebRegistries()
        {
            MvcCmsFormDataActionFilter.GetContainer = AppObjectFactory.GetContainer;

            For<ICmsUrlBuilderFactory>().Singleton().Use<CmsUrlBuilderFactory>();
        }
    }
}
