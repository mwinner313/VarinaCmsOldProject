using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VarinaCmsV2.DomainClasses.Entities.EShop;
using VarinaCmsV2.DomainClasses.Entities.EShop.Discounts;
using VarinaCmsV2.DomainClasses.Entities.EShop.Orders;
using VarinaCmsV2.Security;
using VarinaCmsV2.Security.Contracts;
using VarinaCmsV2.Services.Tests.McokServices;
using VarinaCmsV2.Services.Tests.MockDataServices;
using VarinaCmsV2.Services.WebClientServices;

namespace VarinaCmsV2.Services.Tests.WebClientServicesTests.Disocunt
{
    [TestClass]
    public class DiscountWebClientServiceTests
    {
        //[TestMethod]
        //public async Task TenPercentageOffForClothesCategorySenarioTest()
        //{
        //    CurrentIdentityMocker.Create();
        //    var identityManager = new IdentityManager(new List<IPremissionList>(), new List<IRoleList>());
        //    var mockProductCategoryDataService = new MockProductCategoryDataService();
        //    var mockProductDataService = new MockProductDataService(mockProductCategoryDataService);
        //    var mockWorkContext = new MockWorkContext(mockProductDataService);
        //    var mockDiscountDataService = new MockDiscountDataService();
        //    var mockOrderDataService = new MockOrderDataService();
        //    var mockDiscountUsageDataService = new MockDiscountUsageHistoryDataService();
        //    var discountValidator = new DiscountValidator(mockDiscountDataService, mockDiscountUsageDataService,
        //        mockWorkContext, identityManager, mockProductCategoryDataService, mockProductDataService);
        //    var mockCurrentUserService = new MockCurrentUserService(mockWorkContext, identityManager, discountValidator, mockDiscountDataService);
        //    var discountService = new DiscountWebClientService(mockDiscountDataService, mockCurrentUserService, mockProductDataService, mockProductCategoryDataService, discountValidator, new MockUnitOfWork());
        //    var orderWebClientService = new OrderWebClientService(mockWorkContext, discountService, mockCurrentUserService, mockOrderDataService, mockProductDataService, new MockUnitOfWork());

        //    await mockDiscountDataService.AddAsync(new DiscountSenarios(mockProductCategoryDataService, mockProductDataService).Senario1);

        //    var order = await orderWebClientService.PlaceCurrentUserCartAsync();
        //    Console.WriteLine(order.OrderSubTotal);
        //    Console.WriteLine(order.OrderDiscount);
        //    Console.WriteLine(order.OrderTotal);
        //}

        //[TestMethod]
        //public async Task Senario2Test()
        //{
        //    CurrentIdentityMocker.Create();


        //    var identityManager = new IdentityManager(new List<IPremissionList>(), new List<IRoleList>());
        //    var mockProductCategoryDataService = new MockProductCategoryDataService();
        //    var mockProductDataService = new MockProductDataService(mockProductCategoryDataService);
        //    var mockWorkContext = new MockWorkContext(mockProductDataService);
        //    var mockDiscountDataService = new MockDiscountDataService();
        //    var mockOrderDataService = new MockOrderDataService();
        //    var mockDiscountUsageDataService = new MockDiscountUsageHistoryDataService();
        //    var discountValidator = new DiscountValidator(mockDiscountDataService, mockDiscountUsageDataService,
        //        mockWorkContext, identityManager, mockProductCategoryDataService, mockProductDataService);
        //    var mockCurrentUserService = new MockCurrentUserService(mockWorkContext, identityManager, discountValidator, mockDiscountDataService);
        //    var discountService = new DiscountWebClientService(mockDiscountDataService, mockCurrentUserService, mockProductDataService, mockProductCategoryDataService, discountValidator, new MockUnitOfWork());
        //    var orderWebClientService = new OrderWebClientService(mockWorkContext, discountService, mockCurrentUserService, mockOrderDataService, mockProductDataService, new MockUnitOfWork());

        //    await mockDiscountDataService.AddAsync(new DiscountSenarios(mockProductCategoryDataService, mockProductDataService).Senario2);

        //    var order = await orderWebClientService.PlaceCurrentUserCartAsync();
        //    Console.WriteLine(order.OrderSubTotal);
        //    Console.WriteLine(order.OrderDiscount);
        //    Console.WriteLine(order.OrderTotal);
        //}

        //[TestMethod]
        //public async Task Senario3Test()
        //{
        //    CurrentIdentityMocker.Create();


        //    var identityManager = new IdentityManager(new List<IPremissionList>(), new List<IRoleList>());
        //    var mockProductCategoryDataService = new MockProductCategoryDataService();
        //    var mockProductDataService = new MockProductDataService(mockProductCategoryDataService);
        //    var mockWorkContext = new MockWorkContext(mockProductDataService);
        //    var mockDiscountDataService = new MockDiscountDataService();
        //    var mockOrderDataService = new MockOrderDataService();
        //    var mockDiscountUsageDataService = new MockDiscountUsageHistoryDataService();
        //    var discountValidator = new DiscountValidator(mockDiscountDataService, mockDiscountUsageDataService,
        //        mockWorkContext, identityManager, mockProductCategoryDataService, mockProductDataService);
        //    var mockCurrentUserService = new MockCurrentUserService(mockWorkContext, identityManager, discountValidator, mockDiscountDataService);
        //    var discountService = new DiscountWebClientService(mockDiscountDataService, mockCurrentUserService, mockProductDataService, mockProductCategoryDataService, discountValidator, new MockUnitOfWork());
        //    var orderWebClientService = new OrderWebClientService(mockWorkContext, discountService, mockCurrentUserService, mockOrderDataService, mockProductDataService, new MockUnitOfWork());

        //    await mockDiscountDataService.AddAsync(new DiscountSenarios(mockProductCategoryDataService, mockProductDataService).Senario3);

        //    var order = await orderWebClientService.PlaceCurrentUserCartAsync();
        //    Console.WriteLine(order.OrderSubTotal);
        //    Console.WriteLine(order.OrderDiscount);
        //    Console.WriteLine(order.OrderTotal);
        //}
        //[TestMethod]
        //public async Task Senario4Test()
        //{
        //    CurrentIdentityMocker.Create();


        //    var identityManager = new IdentityManager(new List<IPremissionList>(), new List<IRoleList>());
        //    var mockProductCategoryDataService = new MockProductCategoryDataService();
        //    var mockProductDataService = new MockProductDataService(mockProductCategoryDataService);
        //    var mockWorkContext = new MockWorkContext(mockProductDataService);
        //    var mockDiscountDataService = new MockDiscountDataService();
        //    var mockOrderDataService = new MockOrderDataService();
        //    var mockDiscountUsageDataService = new MockDiscountUsageHistoryDataService();
        //    var discountValidator = new DiscountValidator(mockDiscountDataService, mockDiscountUsageDataService,
        //        mockWorkContext, identityManager, mockProductCategoryDataService, mockProductDataService);
        //    var mockCurrentUserService = new MockCurrentUserService(mockWorkContext, identityManager, discountValidator, mockDiscountDataService);
        //    var discountService = new DiscountWebClientService(mockDiscountDataService, mockCurrentUserService, mockProductDataService, mockProductCategoryDataService, discountValidator, new MockUnitOfWork());
        //    var orderWebClientService = new OrderWebClientService(mockWorkContext, discountService, mockCurrentUserService, mockOrderDataService, mockProductDataService, new MockUnitOfWork());

        //    await mockDiscountDataService.AddAsync(new DiscountSenarios(mockProductCategoryDataService, mockProductDataService).Senario4);

        //    var order = await orderWebClientService.PlaceCurrentUserCartAsync();
        //    Console.WriteLine(order.OrderSubTotal);
        //    Console.WriteLine(order.OrderDiscount);
        //    Console.WriteLine(order.OrderTotal);
        //    foreach (var orderOrderItem in order.OrderItems)
        //    {
        //        Console.WriteLine(orderOrderItem.DiscountAmount);
        //    }
        //}
        //[TestMethod]
        //public async Task Senario5Test()
        //{
        //    CurrentIdentityMocker.Create();

        //    var identityManager = new IdentityManager(new List<IPremissionList>(), new List<IRoleList>());
        //    var mockProductCategoryDataService = new MockProductCategoryDataService();
        //    var mockProductDataService = new MockProductDataService(mockProductCategoryDataService);
        //    var mockWorkContext = new MockWorkContext(mockProductDataService);
        //    var mockDiscountDataService = new MockDiscountDataService();
        //    var mockOrderDataService = new MockOrderDataService();
        //    var mockDiscountUsageDataService = new MockDiscountUsageHistoryDataService();
        //    var discountValidator = new DiscountValidator(mockDiscountDataService, mockDiscountUsageDataService,
        //        mockWorkContext, identityManager, mockProductCategoryDataService, mockProductDataService);
        //    var mockCurrentUserService = new MockCurrentUserService(mockWorkContext, identityManager, discountValidator, mockDiscountDataService);
        //    var discountService = new DiscountWebClientService(mockDiscountDataService, mockCurrentUserService, mockProductDataService, mockProductCategoryDataService, discountValidator, new MockUnitOfWork());
        //    var orderWebClientService = new OrderWebClientService(mockWorkContext, discountService, mockCurrentUserService, mockOrderDataService, mockProductDataService, new MockUnitOfWork());

        //    await mockDiscountDataService.AddAsync(new DiscountSenarios(mockProductCategoryDataService, mockProductDataService).Senario1);

        //    var order = await orderWebClientService.PlaceCurrentUserCartAsync();
        //    Console.WriteLine(order.OrderSubTotal);
        //    Console.WriteLine(order.OrderDiscount);
        //    Console.WriteLine(order.OrderTotal);
        //    foreach (var orderOrderItem in order.OrderItems)
        //    {
        //        Console.WriteLine(orderOrderItem.DiscountAmount);
        //    }
        //}
        //[TestMethod]
        //public async Task CouponSenario1Test()
        //{
        //    CurrentIdentityMocker.Create();

        //    var identityManager = new IdentityManager(new List<IPremissionList>(), new List<IRoleList>());
        //    var mockProductCategoryDataService = new MockProductCategoryDataService();
        //    var mockProductDataService = new MockProductDataService(mockProductCategoryDataService);
        //    var mockWorkContext = new MockWorkContext(mockProductDataService);
        //    var mockDiscountDataService = new MockDiscountDataService();
        //    var mockOrderDataService = new MockOrderDataService();
        //    var mockDiscountUsageDataService = new MockDiscountUsageHistoryDataService();
        //    var discountValidator = new DiscountValidator(mockDiscountDataService, mockDiscountUsageDataService,
        //        mockWorkContext, identityManager, mockProductCategoryDataService, mockProductDataService);
        //    var mockCurrentUserService = new MockCurrentUserService(mockWorkContext, identityManager, discountValidator, mockDiscountDataService);
        //    var discountService = new DiscountWebClientService(mockDiscountDataService, mockCurrentUserService, mockProductDataService, mockProductCategoryDataService, discountValidator, new MockUnitOfWork());
        //    var orderWebClientService = new OrderWebClientService(mockWorkContext, discountService, mockCurrentUserService, mockOrderDataService, mockProductDataService, new MockUnitOfWork());

        //    var discountWithCouponCode =
        //        new DiscountSenarios(mockProductCategoryDataService, mockProductDataService).Senario5;
        //    discountWithCouponCode.RequiresCouponCode = true;
        //    discountWithCouponCode.CouponCode = "111";
        //    var discountWithCouponCode2 =
        //        new DiscountSenarios(mockProductCategoryDataService, mockProductDataService).Senario4;
        //    discountWithCouponCode2.RequiresCouponCode = true;
        //    discountWithCouponCode2.CouponCode = "222";
        //    await mockDiscountDataService.AddAsync(discountWithCouponCode);
        //    await mockDiscountDataService.AddAsync(discountWithCouponCode2);
        //    Assert.IsTrue((await mockCurrentUserService.AddCouponCodeUsage("111")).Succeed);
        //    Assert.IsTrue((await mockCurrentUserService.AddCouponCodeUsage("222")).Succeed);
        //    Console.Write((await mockCurrentUserService.AddCouponCodeUsage("22222")).Message);

        //    await mockDiscountDataService.AddAsync(discountWithCouponCode);

        //    var order = await orderWebClientService.PlaceCurrentUserCartAsync();
        //    Console.WriteLine(order.OrderSubTotal);
        //    Console.WriteLine(order.OrderDiscount);
        //    Console.WriteLine(order.OrderTotal);
        //    foreach (var orderOrderItem in order.OrderItems)
        //    {
        //        Console.WriteLine(orderOrderItem.DiscountAmount);
        //    }
        //}

        //[TestMethod]
        //public async Task CouponSenario1PostPaymentTest()
        //{
        //    CurrentIdentityMocker.Create();

        //    var identityManager = new IdentityManager(new List<IPremissionList>(), new List<IRoleList>());
        //    var mockProductCategoryDataService = new MockProductCategoryDataService();
        //    var mockProductDataService = new MockProductDataService(mockProductCategoryDataService);
        //    var mockWorkContext = new MockWorkContext(mockProductDataService);
        //    var mockDiscountDataService = new MockDiscountDataService();
        //    var mockOrderDataService = new MockOrderDataService();
        //    var mockDiscountUsageDataService = new MockDiscountUsageHistoryDataService();
        //    var discountValidator = new DiscountValidator(mockDiscountDataService, mockDiscountUsageDataService,
        //        mockWorkContext, identityManager, mockProductCategoryDataService, mockProductDataService);
        //    var mockCurrentUserService = new MockCurrentUserService(mockWorkContext, identityManager, discountValidator, mockDiscountDataService);
        //    var discountService = new DiscountWebClientService(mockDiscountDataService, mockCurrentUserService, mockProductDataService, mockProductCategoryDataService, discountValidator, new MockUnitOfWork());
        //    var orderWebClientService = new OrderWebClientService(mockWorkContext, discountService, mockCurrentUserService, mockOrderDataService, mockProductDataService, new MockUnitOfWork());

        //    var discountWithCouponCode =
        //        new DiscountSenarios(mockProductCategoryDataService, mockProductDataService).Senario5;
        //    discountWithCouponCode.RequiresCouponCode = true;
        //    discountWithCouponCode.CouponCode = "111";
        //    var discountWithCouponCode2 =
        //        new DiscountSenarios(mockProductCategoryDataService, mockProductDataService).Senario4;
        //    discountWithCouponCode2.RequiresCouponCode = true;
        //    discountWithCouponCode2.CouponCode = "222";
        //    await mockDiscountDataService.AddAsync(discountWithCouponCode);
        //    await mockDiscountDataService.AddAsync(discountWithCouponCode2);

        //    await mockDiscountDataService.AddAsync(discountWithCouponCode);

        //    var order = await orderWebClientService.PlaceCurrentUserCartAsync();
        //    Console.WriteLine(order.OrderSubTotal);
        //    Console.WriteLine(order.OrderDiscount);
        //    Console.WriteLine(order.OrderTotal);
        //    foreach (var orderOrderItem in order.OrderItems)
        //    {
        //        Console.WriteLine(orderOrderItem.DiscountAmount);
        //    }

        //    order= await orderWebClientService.PostProcessOrderPayment(order.Id);

        //    Assert.AreEqual(order.PaymentStatus,PaymentStatus.Paid);
        //    Assert.AreEqual(order.OrderStatus,OrderStatus.Processing);
        //}
    }

}
