using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using VarinaCmsV2.Common;
using VarinaCmsV2.Core.Contracts;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.Core.Contracts.Security;
using VarinaCmsV2.Core.Contracts.WebClientServices.Discounts;
using VarinaCmsV2.Core.Contracts.WebClientServices.Products;
using VarinaCmsV2.Core.Services.Settings;
using VarinaCmsV2.Data.Contracts;
using VarinaCmsV2.DomainClasses.Entities;
using VarinaCmsV2.DomainClasses.Entities.EShop;
using VarinaCmsV2.DomainClasses.Entities.EShop.Orders;
using VarinaCmsV2.Security.Contracts;
using VarinaCmsV2.Services.WebClientServices;

namespace VarinaCmsV2.Services.Tests.WebClientServicesTests
{
    [TestClass]
    public class ProductWebClientServiceTests
    {
        private readonly Mock<IRestrictedItemAccessManager> _accessManager = new Mock<IRestrictedItemAccessManager>();

        private readonly Mock<IDiscountWebClientService> _discountWebClientService =
            new Mock<IDiscountWebClientService>();

        private readonly Mock<IEntitySchemeDataService> _entitySchemeDataService = new Mock<IEntitySchemeDataService>();
        private readonly Mock<IIdentityManager> _identityManager = new Mock<IIdentityManager>();
        private readonly Mock<IOrderDataService> _orderDateService = new Mock<IOrderDataService>();

        private readonly Mock<IProductCategoryDataService> _productCategoryDataService =
            new Mock<IProductCategoryDataService>();

        private readonly Mock<IProductDataService> _productDataService = new Mock<IProductDataService>();

        private readonly Mock<IProductReviewDataService> _productReviewDataService =
            new Mock<IProductReviewDataService>();

        private readonly Mock<ISecurityLogger> _securityLogger = new Mock<ISecurityLogger>();
        private readonly Mock<ISettingService> _settings = new Mock<ISettingService>();
        private readonly Mock<ITagDataService> _tagDataService = new Mock<ITagDataService>();
        private readonly Mock<IUnitOfWork> _unitOfWork = new Mock<IUnitOfWork>();
        private readonly Mock<IWorkContext> _workContext = new Mock<IWorkContext>();
        private ProductWebClientService _productWebClientService;

        public ProductWebClientServiceTests()

        {
            _identityManager.Setup(o => o.GetCurrentIdentity()).Returns(CurrentIdentityMocker.Get());


            _productWebClientService = new ProductWebClientService(_productDataService.Object,
                _accessManager.Object,
                _productCategoryDataService.Object,
                _workContext.Object,
                _unitOfWork.Object,
                _productReviewDataService.Object,
                _securityLogger.Object,
                _orderDateService.Object,
                new ShoppingCartHelper(),
                _discountWebClientService.Object, _tagDataService.Object, _settings.Object, _identityManager.Object,
                _entitySchemeDataService.Object
            );

        }

        [TestMethod]
        public async Task Should_Return_NotExistsFile_ForDownload()
        {
            //arange
            var orderId = Guid.NewGuid();
            var productId = Guid.NewGuid();

            _productDataService.SetupGet(o => o.Query)
                .Returns(() => new List<Product>() { new Product() }.AsQueryable());

            //act
            var res = await _productWebClientService.GetProductForDownLoad(orderId, productId);

            //assert
            Assert.AreEqual(res.Status, DownloadResponseStatus.FileNotExists);
        }

        [TestMethod]
        public async Task ShouldReturnUserCantDownload_Due_toNotPurchasing_Product()
        {
            //arange
            var orderId = Guid.NewGuid();
            var productId = Guid.NewGuid();

            _productDataService.SetupGet(o => o.Query)
                .Returns(() => new List<Product>() { new Product() { IsDownLoadFile = true, File = new FileData(), Id = productId } }.AsQueryable());
            _orderDateService.SetupGet(o => o.Query)
                .Returns(new List<Order>() {}.AsQueryable());
            //act
            var res = await _productWebClientService.GetProductForDownLoad(orderId, productId);

            //assert
            Assert.AreEqual(res.Status, DownloadResponseStatus.DownLoadNotAllowedDueToOrderNotExists);
        }
    }
}