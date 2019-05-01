using System;
using System.Collections.Generic;
using System.Linq;
using VarinaCmsV2.Common;
using VarinaCmsV2.Core.Contracts.DataServices;

namespace VarinaCmsV2.Core.Logic.Services.Dashboard.WidgetDataCreators
{
    public class BestSellingProductWidgetDataCreator : IDashboardWidgetDataCreator
    {
        private readonly IProductDataService _productDataService;

        public BestSellingProductWidgetDataCreator(IProductDataService productDataService)
        {
            _productDataService = productDataService;
        }

        public string Name => "BestSellings";

        public object CreateData()
        {
            return new Dictionary<string, object>()
            {
                {
                    "بر حسب تعداد خریدار",
                    CreateByUserCount()
                } ,
                {
                    "برحسب تعداد فروخته شده",
                    CreateByQuantity()
                }
            };
        }

        private object CreateByQuantity()
        {
            return _productDataService.Query
                .OrderByDescending(x => x.OrderItems.Sum(i => i.Quantity))
                .Select(x => new { x.Title, Count = x.OrderItems.Sum(i => i.Quantity), TotalAmount = x.OrderItems.Sum(i => i.Price) }).Take(10).ToList();
        }

        private object CreateByUserCount()
        {
            return _productDataService.Query
                .OrderByDescending(x => x.OrderItems.Count())
                .Select(x => new { x.Title, Count = x.OrderItems.Count(),TotalAmount=x.OrderItems.Sum(i=>i.Price) }).Take(10).ToList();
        }
    }
}
