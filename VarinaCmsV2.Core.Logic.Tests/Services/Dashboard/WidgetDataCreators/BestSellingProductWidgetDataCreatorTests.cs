using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json.Linq;
using VarinaCmsV2.Common;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.Core.Logic.Services.Dashboard.WidgetDataCreators;
using VarinaCmsV2.DomainClasses.Entities.EShop;
using VarinaCmsV2.DomainClasses.Entities.EShop.Orders;

namespace VarinaCmsV2.Core.Logic.Tests.Services.Dashboard.WidgetDataCreators
{
    [TestClass]
    public class BestSellingProductWidgetDataCreatorTests
    {
        [TestMethod]
        public void Should_CreateBaseOnDateRange()
        {
            //arrange
            var mProductDataService = new Mock<IProductDataService>();
            mProductDataService.SetupGet(x => x.Query).Returns(new List<Product>()
            {
                CreateProductWithOrderItemsAndOrder("شلوار",15,DateHelper.GetStartOfMonthFa()),
                CreateProductWithOrderItemsAndOrder("نخ دندان",1,DateHelper.GetStartOfYearFa()),
                CreateProductWithOrderItemsAndOrder("کلاه",12,DateHelper.GetStartOfYearFa()),
                CreateProductWithOrderItemsAndOrderWithOrderItemCount("کلاه1",12,DateHelper.GetStartOfMonthFa()),
                CreateProductWithOrderItemsAndOrderWithOrderItemCount("کلاه55",12,DateHelper.GetStartOfWeekFa().AddDays(-2)),
            }.AsQueryable());
            var creator = new BestSellingProductWidgetDataCreator(mProductDataService.Object);
            //act assert
            Console.Write(JObject.FromObject(creator.CreateData()));
        }

        private Product CreateProductWithOrderItemsAndOrderWithOrderItemCount(string title, int orderItemsCount, DateTime dateTime)
        {
            var product = new Product() { Title = title, OrderItems = new List<OrderItem>() };
            for (int i = 0; i < orderItemsCount; i++)
            {
                product.OrderItems.Add(new OrderItem() { Order = new Order() { CreateDateTime = dateTime.AddDays(i)  },Quantity = i});
            }
            return product;
        }

        private Product CreateProductWithOrderItemsAndOrder(string title, int orderItemsCount, DateTime getStartOfMonthFa)
        {
            var product = new Product() { Title = title, OrderItems = new List<OrderItem>() };
            for (int i = 0; i < orderItemsCount; i++)
            {
                product.OrderItems.Add(new OrderItem() { Order = new Order() { CreateDateTime = getStartOfMonthFa.AddDays(i) } });
            }
            return product;
        }
    }
}
