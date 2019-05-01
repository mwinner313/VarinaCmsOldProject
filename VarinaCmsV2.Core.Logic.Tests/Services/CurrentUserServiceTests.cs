//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Text;
//using System.Threading.Tasks;
//using Microsoft.AspNet.Identity;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Moq;
//using VarinaCmsV2.Common;
//using VarinaCmsV2.Core.Contracts;
//using VarinaCmsV2.Core.Contracts.DataServices;
//using VarinaCmsV2.Core.Contracts.Security;
//using VarinaCmsV2.Core.Contracts.WebClientServices.Discounts;
//using VarinaCmsV2.Core.Logic.Services.CurrentUser;
//using VarinaCmsV2.Core.Services.Settings;
//using VarinaCmsV2.Data.Contracts;
//using VarinaCmsV2.DomainClasses.Entities.EShop.Orders;
//using VarinaCmsV2.FileManager.Files;
//using VarinaCmsV2.Security.Contracts;
//using VarinaCmsV2.Services.Tests;

//namespace VarinaCmsV2.Core.Logic.Tests.Services
//{
//    [TestClass]
//    public class CurrentUserServiceTests
//    {
//        private readonly Mock<IRestrictedItemAccessManager> _accessManager = new Mock<IRestrictedItemAccessManager>();

//        private readonly Mock<IDiscountWebClientService> _discountWebClientService =
//            new Mock<IDiscountWebClientService>();

//        private readonly Mock<IEntitySchemeDataService> _entitySchemeDataService = new Mock<IEntitySchemeDataService>();
//        private readonly Mock<IIdentityManager> _identityManager = new Mock<IIdentityManager>();
//        private readonly Mock<IOrderDataService> _orderDateService = new Mock<IOrderDataService>();

//        private readonly Mock<IProductCategoryDataService> _productCategoryDataService =
//            new Mock<IProductCategoryDataService>();

//        private readonly Mock<IProductDataService> _productDataService = new Mock<IProductDataService>();

//        private readonly Mock<IProductReviewDataService> _productReviewDataService =
//            new Mock<IProductReviewDataService>();

//        private readonly Mock<ISecurityLogger> _securityLogger = new Mock<ISecurityLogger>();
//        private readonly Mock<IFileManager> _fileManager = new Mock<IFileManager>();
//        private readonly Mock<IAppUserManager> _userManager = new Mock<IAppUserManager>();
//        private readonly Mock<ISettingService> _settings = new Mock<ISettingService>();
//        private readonly Mock<IDiscountDataService> _discountDataService = new Mock<IDiscountDataService>();
//        private readonly Mock<ITagDataService> _tagDataService = new Mock<ITagDataService>();
//        private readonly Mock<IShoppingCartItemDataService> _shoppingCartItemDataService = new Mock<IShoppingCartItemDataService>();
//        private readonly Mock<IUnitOfWork> _unitOfWork = new Mock<IUnitOfWork>();
//        private readonly Mock<IWorkContext> _workContext = new Mock<IWorkContext>();
//        private readonly Mock<IDiscountValidator> _discountValidator = new Mock<IDiscountValidator>();
//        private readonly Mock<ICouponCodeDataService> _couponCodeDataService = new Mock<ICouponCodeDataService>();
//        private readonly Mock<IOrderDataService> _orderDataService = new Mock<IOrderDataService>();
//        public CurrentUserServiceTests()
//        {

//        }

//        [TestMethod]
//        public async Task ShouldPageinageUserOrders()
//        {
//            var currentUserId = CurrentIdentityMocker.Get().GetUserId();

//            var srv = new CurrentUserService(_identityManager.Object, _userManager.Object, _unitOfWork.Object,
//                _fileManager.Object, _discountDataService.Object, _workContext.Object
//                , _shoppingCartItemDataService.Object, _discountValidator.Object, _couponCodeDataService.Object, _orderDataService.Object);
//            var mockData = CreateOrder(70, currentUserId.ToGuid());

//            _orderDataService.Setup(x => x.Query).Returns(mockData.AsQueryable);
//            _userManager.Setup(x => x.GetCurrentUserId()).Returns(currentUserId.ToGuid);
//            List<Order> res = await srv.GetOrders(1, 12);

//            res.ForEach(x => Assert.IsTrue(mockData.Contains(x)));
//            Assert.IsTrue(res.Count <= 12);
//            Assert.IsTrue(res.Count !=0);
//            _orderDataService.VerifyGet(x=>x.Query);
//            _userManager.Verify(x=>x.GetCurrentUserId());
//        }

//        private List<Order> CreateOrder(int count, Guid creatorId)
//        {
//            var retVal = new List<Order>();
//            for (int i = 0; i < count; i++)
//            {
//                retVal.Add(new Order()
//                {
//                    CreatorId = creatorId
//                });
//            }
//            return retVal;
//        }
//    }
//}
