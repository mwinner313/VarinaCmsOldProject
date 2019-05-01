using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using VarinaCmsV2.Common;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.DomainClasses.Entities.EShop.Orders;

namespace VarinaCmsV2.Core.Logic.Services.Dashboard.WidgetDataCreators
{
    public class OrderTotalsTableDataCreator : IDashboardWidgetDataCreator
    {
        private readonly IOrderDataService _orderDataService;

        public OrderTotalsTableDataCreator(IOrderDataService orderDataService)
        {
            _orderDataService = orderDataService;
        }

        public string Name => "OrderTotals";
        public object CreateData()
        {

            var retVal = new List<OrdersOverView>();
            retVal.AddRange(GetOverViewFrom(DateHelper.GetStartOfDayFa(), "امروز"));
            retVal.AddRange(GetOverViewFrom(DateHelper.GetStartOfWeekFa(), "این هفته"));
            retVal.AddRange(GetOverViewFrom(DateHelper.GetStartOfMonthFa(), "این ماه"));
            retVal.AddRange(GetOverViewFrom(DateHelper.GetStartOfYearFa(), "امسال"));
            retVal.AddRange(GetOverViewFrom(DateTime.MinValue, "تا بحال"));

            return new
            {
                Statuses = retVal.Select(x => x.Status).Distinct().ToList(),
                Ranges = retVal.Select(x => x.Range).Distinct().ToList(),
                Datas = retVal
            };
        }

        private List<OrdersOverView> GetOverViewFrom(DateTime @from, string range)
        {
            var retVal = new List<OrdersOverView>();
            var orders = _orderDataService.Query.Where(x => x.CreateDateTime > @from).Select(x => new { x.OrderTotal, x.OrderStatus })
                 .ToList();

            foreach (var value in Enum.GetNames(typeof(OrderStatus)))
            {
                Enum.TryParse(value, true, out OrderStatus status);
                var overView = new OrdersOverView()
                {
                    Range = range,
                    Status = status,
                    OrderTotal = orders.Where(x => x.OrderStatus == status).Sum(x => x.OrderTotal)
                };
                retVal.Add(overView);
            }

            return retVal;
        }

        private class OrdersOverView
        {
            public OrderStatus Status { get; set; }
            public long OrderTotal { get; set; }
            public string Range { get; set; }
        }
    }
}

