namespace VarinaCmsV2.Core.Contracts.Configuration.Base
{
    public interface IAppConfigurationElement
    {
        string Name { get; set; }
        string Value { get; set; }
    }
}