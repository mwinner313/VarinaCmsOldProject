namespace VarinaCmsV2.Common.Web
{
    public partial interface IUserAgentHelper
    {
        bool IsSearchEngine();
        bool IsMobileDevice();
        bool IsIe8();
    }
}
