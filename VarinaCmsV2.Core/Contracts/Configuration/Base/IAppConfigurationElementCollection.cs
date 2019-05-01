namespace VarinaCmsV2.Core.Contracts.Configuration.Base
{
    public interface IAppConfigurationElementCollection
    {
        string Get(string name);
        void Set(string name, string value);
        void Add(IAppConfigurationElement el);
    }
}