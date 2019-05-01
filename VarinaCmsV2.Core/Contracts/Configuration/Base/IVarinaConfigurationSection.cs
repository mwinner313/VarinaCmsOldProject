using System.Configuration;

namespace VarinaCmsV2.Core.Contracts.Configuration.Base
{
    public interface IVarinaConfigurationSection
    {
        IAppConfigurationElementCollection AppInfo { get; }
        ProviderSettingsCollection  Modules { get; }
        void SaveChanges();
    }
}