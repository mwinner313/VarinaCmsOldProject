using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StructureMap;
using VarinaCmsV2.Core.Contracts.Configuration.Base;

namespace VarinaCmsV2.Core.Modules
{
    public class CmsModuleProvider:ICmsModuleProvider
    {
        private readonly IVarinaConfigurationSection _configuration;
        private readonly IContainer _container;
        public CmsModuleProvider(IVarinaConfigurationSection configuration,IContainer container)
        {
            _configuration = configuration;
            _container = container;
        }

        public void InitializeInstalledModules()
        {
            foreach (ProviderSettings module in _configuration.Modules)
            {
                HandleModuleIntialization(module);
            }
        }

        private void HandleModuleIntialization(ProviderSettings moduleSetting)
        {
            var module = Activator.CreateInstance(Type.GetType(moduleSetting.Type)) as BaseModule;
            if (module == null) throw new MisingModuleDllException(moduleSetting.Name);

            module.Initialize(moduleSetting.Parameters,_container);
            module.SetUpDataBase();
        }
    }
}
