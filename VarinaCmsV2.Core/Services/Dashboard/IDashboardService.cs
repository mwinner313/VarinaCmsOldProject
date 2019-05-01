using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace VarinaCmsV2.Core.Services.Dashboard
{
    public interface IDashboardService
    {
        List<DashboardWidgetData> GetDashboardWidgetsData();
    }
}
