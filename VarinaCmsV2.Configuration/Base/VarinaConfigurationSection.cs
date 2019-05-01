using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.Core.Contracts.Configuration.Base;

namespace VarinaCmsV2.Configuration.Base
{
    public class VarinaConfigurationSection : ConfigurationSection, IVarinaConfigurationSection
    {
        [ConfigurationProperty("appInfo")]
        [ConfigurationCollection(typeof(AppConfigurationElementCollection), AddItemName = "add")]
        private AppConfigurationElementCollection _appInfo => this["appInfo"] as AppConfigurationElementCollection;
      
        public IAppConfigurationElementCollection AppInfo => _appInfo;
        [ConfigurationProperty("modules", IsRequired = true)]
        public ProviderSettingsCollection Modules => (ProviderSettingsCollection) base["modules"];
        public void SaveChanges()
        {
            CurrentConfiguration.Save(ConfigurationSaveMode.Full);
        }
    }
}
