using Newtonsoft.Json.Linq;

namespace VarinaCmsV2.Core.Logic.Services.Dashboard
{
    public interface IDashboardWidgetDataCreator
    {
        string Name { get; }
        object CreateData();
    }
}