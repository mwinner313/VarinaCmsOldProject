namespace VarinaCmsV2.Core.Contracts.Configuration
{
    public interface ICmsConfiguration
    {
        string Version { get; }
        string ContentRelativePath { get; }
        string ScriptsRelativePath { get; }
        string UiDeveloperFileRelativePath { get; }
        string WorkingDirectoryRootPath { get; }
    }
}
