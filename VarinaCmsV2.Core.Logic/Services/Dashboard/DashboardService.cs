using System.Collections.Generic;
using VarinaCmsV2.Core.Services.Dashboard;

namespace VarinaCmsV2.Core.Logic.Services.Dashboard
{
    public class DashboardService : IDashboardService
    {
        private readonly List<IDashboardWidgetDataCreator> _dataCreators;

        public DashboardService(List<IDashboardWidgetDataCreator> dataCreators)
        {
            _dataCreators = dataCreators;
        }

        public List<DashboardWidgetData> GetDashboardWidgetsData()
        {
            var retVal = new List<DashboardWidgetData>();

            foreach (var dataCreator in _dataCreators)
            {
                retVal.Add(new DashboardWidgetData()
                {
                    Data = dataCreator.CreateData(),
                    Name = dataCreator.Name
                });
            }

            return retVal;
        }
    }
}
