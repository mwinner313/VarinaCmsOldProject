using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.DomainClasses.Entities.EShop;
using VarinaCmsV2.DomainClasses.Entities.EShop.Discounts;

namespace VarinaCmsV2.Services.Tests.MockDataServices
{
    public class DiscountSenarios
    {
        private readonly MockProductCategoryDataService _mockProductCategoryDataService;
        private readonly MockProductDataService _productDataService;
        public Discount Senario1 { get; set; }
        public Discount Senario2 { get; set; }
        public Discount Senario3 { get; set; }
        public Discount Senario4 { get; set; }
        public Discount Senario5 { get; set; }
        public DiscountSenarios(MockProductCategoryDataService mockProductCategoryDataService, MockProductDataService productDataService)
        {
            _mockProductCategoryDataService = mockProductCategoryDataService;
            _productDataService = productDataService;
            Initialize();
        }

        private void Initialize()
        {
            Senario1 = new Discount()
            {
                Name = "تخفیف 10 درضدی  تمامی کاربران دسته بندی لباس",
                UsePercentage = true,
                DiscountPercentage = 10,
                CouponCodes = new List<CouponCode>(),
                CouponCodeType = CouponCodeType.Single,
                AppliedToSubCategories = true,
                DiscountType = DiscountType.AssignedToCategories,
                AppliedToCategories = new List<ProductCategory>()
                {
                    _mockProductCategoryDataService.Query.First(x =>
                        x.Id == _mockProductCategoryDataService.ClothesCatId)
                },
                MaximumDiscountedQuantity = 5,
                LimitationTimes = 1,
                LimitationType = DiscountLimitationType.NTimesPerCustomer
            };
            foreach (var appliedToCategory in Senario1.AppliedToCategories)
            {
                appliedToCategory.AppliedDiscounts.Add(Senario1);
            }



            Senario2 = new Discount()
            {
                Name = "25 هزار تومان برای سبد خرید های بالای 200 هزار تومن",
                LimitationType = DiscountLimitationType.Unlimited,
                DiscountType = DiscountType.AssignedToOrderSubTotal,
                DiscountAmount = 25,
                CouponCodes = new List<CouponCode>(),
                CouponCodeType = CouponCodeType.Single,
                MinimumCartAmountRequirement = 200,
            };

            Senario3 = new Discount()
            {
                Name = "50 درصد تخفیف خرید بالای 500.",
                UsePercentage = true,
                DiscountType = DiscountType.AssignedToOrderSubTotal,
                DiscountPercentage = 50,
                MinimumCartAmountRequirement = 7000,
                CouponCodes = new List<CouponCode>(),
                CouponCodeType = CouponCodeType.Single,
                LimitationType = DiscountLimitationType.Unlimited,
            };

            Senario4 = new Discount()
            {
                Name = "تخفیف 10 درصدی لباس و لبتاب برای 2 عدد",
                UsePercentage = true,
                DiscountPercentage = 10,
                LimitationType = DiscountLimitationType.Unlimited,
                CouponCodes = new List<CouponCode>(),
                CouponCodeType = CouponCodeType.Single,
                MaximumDiscountedQuantity = 3,
                DiscountType = DiscountType.AssignedToSkus,
                AppliedToProducts = new List<Product>()
                {
                    _productDataService.Query.First(x=>x.Id==_productDataService.ClotheId),
                    _productDataService.Query.First(x=>x.Id==_productDataService.LaptopId),
                }
            };
            foreach (var product in Senario4.AppliedToProducts)
            {
                product.AppliedDiscounts.Add(Senario4);
            }
            Senario5 = new Discount()
            {
                Name = "300تخفیف لبتاب به یک عدد",
                DiscountAmount = 300,
                LimitationType = DiscountLimitationType.Unlimited,
                CouponCodes = new List<CouponCode>(),
                CouponCodeType = CouponCodeType.Single,
                MaximumDiscountedQuantity = 1,
                DiscountType = DiscountType.AssignedToSkus,
                AppliedToProducts = new List<Product>()
                {
                    _productDataService.Query.First(x=>x.Id==_productDataService.LaptopId),
                }

            };
            foreach (var product in Senario5.AppliedToProducts)
            {
                product.AppliedDiscounts.Add(Senario5);
            }
        }

    }
}
