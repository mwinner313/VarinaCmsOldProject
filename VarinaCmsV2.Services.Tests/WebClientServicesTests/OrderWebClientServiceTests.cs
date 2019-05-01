using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VarinaCmsV2.Security;
using VarinaCmsV2.Security.Contracts;
using VarinaCmsV2.Services.Tests.McokServices;
using VarinaCmsV2.Services.Tests.MockDataServices;
using VarinaCmsV2.Services.WebClientServices;

namespace VarinaCmsV2.Services.Tests.WebClientServicesTests
{
    [TestClass]
    public class OrderWebClientServiceTests
    {
        //[TestMethod]
        //public async Task ShouldPlaceCurrentUserCartWithNoDiscounts()
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
        //    var discountService = new DiscountWebClientService(mockDiscountDataService, mockCurrentUserService, mockProductDataService, mockProductCategoryDataService, discountValidator,new MockUnitOfWork());
        //    var orderWebClientService = new OrderWebClientService(mockWorkContext, discountService, mockCurrentUserService, mockOrderDataService, mockProductDataService,new MockUnitOfWork());

        //    mockDiscountDataService.ClearItems();

        //    var order = await orderWebClientService.PlaceCurrentUserCartAsync();

        //    Console.WriteLine(new JavaScriptSerializer().Serialize(mockOrderDataService.Query.ToList()));
        //}
    }
}
