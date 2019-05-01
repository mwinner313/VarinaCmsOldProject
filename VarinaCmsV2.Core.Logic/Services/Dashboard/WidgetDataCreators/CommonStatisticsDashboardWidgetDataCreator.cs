using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.Core.Security.Premissions;
using VarinaCmsV2.DomainClasses.Entities.EShop;
using VarinaCmsV2.DomainClasses.Entities.EShop.Orders;
using VarinaCmsV2.DomainClasses.Entities.EShop.Shipping;
using VarinaCmsV2.Security.Contracts;

namespace VarinaCmsV2.Core.Logic.Services.Dashboard.WidgetDataCreators
{
    public class CommonStatisticsDashboardWidgetDataCreator : IDashboardWidgetDataCreator
    {
        private readonly IOrderDataService _orderDataService;
        private readonly IUserDataService _userDataService;
        private readonly IAppRoleManager _roleManager;
        private readonly IProductDataService _productDataService;
        public CommonStatisticsDashboardWidgetDataCreator(IOrderDataService orderDataService, IUserDataService userDataService, IAppRoleManager roleManager, IProductDataService productDataService)
        {
            _orderDataService = orderDataService;
            _userDataService = userDataService;
            _roleManager = roleManager;
            _productDataService = productDataService;
        }

        public string Name => "CommonStatistics";
        public object CreateData()
        {
            var retVal = new CommonStatisticsData()

            {
                AllOrdersCount = _orderDataService.Query.LongCount(),
                AllRegisterdUsersCount = GetAllRegisteredUsers(),
                NotTrackedOrders =_orderDataService.Query.LongCount(x=>!x.SeenByAdmin),
                ProductsInMinStockQuantityCount = _productDataService.Query.LongCount(x => x.Inventory.MinStockQuantity > x.Inventory.StockQuantity &&
                x.Inventory.TrackMethod==InventoryTrackMethod.TrackByAvalibleQuantity)
            };

            return retVal;
        }

     

        private long GetAllRegisteredUsers()
        {
            var webSiteUserRoleId = _roleManager.FindByName(PreDefRoles.WebSiteUser).Id;
            return _userDataService.Query.LongCount(x => x.Roles.Any(r => r.RoleId == webSiteUserRoleId));
        }

        public class CommonStatisticsData
        {
            public long AllOrdersCount { get; set; }
            public long AllRegisterdUsersCount { get; set; }
            public long ProductsInMinStockQuantityCount { get; set; }
            public long NotTrackedOrders { get; set; }
        }
    }
}
