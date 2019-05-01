using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using StructureMap;
using StructureMap.Web;
using VarinaCmsV2.Common;
using VarinaCmsV2.Common.CustomFields;
using VarinaCmsV2.Core.Logic.Providers;

namespace VarinaCmsV2.IocCofig.ContractRegistries
{
    public class ProvidersRegistries : Registry
    {
        public ProvidersRegistries()
        {
            //ToDo test singleton
            For<CustomFieldFactoryProvider<JObject>>()
                .HybridHttpOrThreadLocalScoped()
                .Use<JsonCustomFieldFactoryProvider>();

            For<List<CustomFieldFactory<JObject>>>()
                .Singleton()
                .Use("find factories", TypeHelper.FindListOf<CustomFieldFactory<JObject>>);
        }
    }
}