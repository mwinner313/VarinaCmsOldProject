namespace VarinaCmsV2.Configuration.Base
{
    public interface IAppConfigurationElement
    {
        string Name { get; set; }
        string Value { get; set; }
    }
}