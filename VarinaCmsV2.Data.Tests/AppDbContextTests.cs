using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VarinaCmsV2.Common;

using VarinaCmsV2.Core.Infrastructure;
using VarinaCmsV2.Core.Infrastructure.GlobalFilters;
using VarinaCmsV2.Core.Logic.Security;
using VarinaCmsV2.Core.Security.Premissions;
using VarinaCmsV2.Data.DbContexts;
using VarinaCmsV2.DomainClasses.Entities;
using VarinaCmsV2.DomainClasses.Entities.EShop;
using VarinaCmsV2.DomainClasses.Entities.EShop.Discounts;
using VarinaCmsV2.DomainClasses.Entities.EShop.Orders;
using VarinaCmsV2.DomainClasses.Users;
using VarinaCmsV2.Security;
using VarinaCmsV2.Security.Contracts;

namespace VarinaCmsV2.Data.Tests
{
    [TestClass]
    public class ClassNameTest
    {
        [TestMethod]
        public void ShouldGetCompeleteName()
        {
            typeof(BaseEntity).Assembly
                   .GetTypes()
                   .Where(typeof(DbEntity).IsAssignableFrom)
                   .ToList()
                   .ForEach(Console.WriteLine);
        }

        [TestMethod]
        public void ShouldFilterBaseOnRestrictedAccses()
        {
            var identity = new ClaimsIdentity();
            identity.AddClaim(new Claim(CustomClaimTypes.Premission, PagePremision.CanSee));
            identity.AddClaim(new Claim(ClaimTypes.Role, "superviser"));
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, "12EA9E35-6B19-44D2-981C-2A8A3E6318E7"));
            Thread.CurrentPrincipal = new ClaimsPrincipal(identity);

            var context = new AppDbContext(new List<GlobalFilter>()
            {
                new SoftDeletibleItemGlobalFilter(),
            });

            var pages = context.Set<Page>();
            Console.WriteLine(pages.Count());
            Console.WriteLine(pages);

            var restrictor =
                new ItemAccessManager(new IdentityManager(new List<IPremissionList>(), new List<IRoleList>()));
            var filtered = restrictor.Filter(pages);
            Console.WriteLine(filtered.Count());
            Console.WriteLine(filtered);
        }

        [TestMethod]
        public void Lazy()
        {
            Assert.AreEqual(new AppDbContext().Set<User>().FirstOrDefault(x => x.UserName == "m.winner313@gmail.com")?.Roles.Count, 1);
        }
        [TestMethod]
        public void AddMockProductData()
        {
            var context = new AppDbContext(new List<GlobalFilter>());
            context.Set<ProductCategory>().Add(new ProductCategory()
            {
                Title = "test",

            });
            context.SaveChanges(); ;
            string optionFilePath = "../../MOCK_DATA.json";
            string json = System.IO.File.ReadAllText(optionFilePath);
            JavaScriptSerializer ser = new JavaScriptSerializer();
            var user = context.Set<User>().OrderBy(o => Guid.NewGuid()).First();
            var cat = context.Set<ProductCategory>().OrderBy(o => new Guid()).First();
            var scheme = context.Set<EntityScheme>().OrderBy(o => new Guid()).First();
            ser.Deserialize<List<Product>>(json).Take(50).ToList().ForEach(x =>
            {
                x.Creator = user;
                x.PrimaryCategory = cat;
                x.Scheme = scheme;
                context.Set<Product>().Add(x);
            });

            context.SaveChanges();
        }
        [TestMethod]
        public void AddMockOrderData()
        {
            var context = new AppDbContext(new List<GlobalFilter>());
            var discounts = context.Set<Discount>().Take(10).ToList();
            string optionFilePath = "../../MOCK_DATAorder.json";
            string json = System.IO.File.ReadAllText(optionFilePath);
            JavaScriptSerializer ser = new JavaScriptSerializer();
            var users = context.Set<User>().ToList();
            var products = context.Set<Product>().Take(100).ToList();
            var items = ser.Deserialize<List<Order>>(json);


            items.ForEach(x =>
        {
            x.Creator = users.OrderBy(o => Guid.NewGuid()).First();
            x.PaymentMethod = "1";
            x.ShippingMethod = "1";
            x.OrderItems = new List<OrderItem>();
            x.DiscountUsageHistories=new List<DiscountUsageHistory>();
            discounts.ForEach(d =>
            {
                x.DiscountUsageHistories.Add(new DiscountUsageHistory()
                {
                    DiscountId = d.Id,
                    CreateDateTime = DateTime.Now,
                });
            });
            foreach (var product in products)
            {
                x.OrderItems.Add(new OrderItem()
                {
                    DiscountAmount = new Random().Next(1000),
                    Quantity = new Random().Next(10),
                    ProductId = product.Id,
                    IsDownloadActivated = true,
                    Price = new Random().Next(500000),
                    UnitPrice = product.Price,
                    DownloadCount = new Random().Next(10),
                    
                });
            }
        });
            context.Set<Order>().AddRange(items.Take(100).ToList());

            context.SaveChanges();
        }
        [TestMethod]
        public void AddMockDiscountData()
        {
            var context = new AppDbContext(new List<GlobalFilter>());

            string optionFilePath = "../../discounts.json";
            string json = System.IO.File.ReadAllText(optionFilePath);
            JavaScriptSerializer ser = new JavaScriptSerializer();
            var products = context.Set<Product>().Take(100).ToList();
            var items = ser.Deserialize<List<Discount>>(json);


            items.ForEach(x =>
            {
                x.DiscountType = GetRandomDiscountType();
                x.LimitationType = GetRandomLimitType();
            });
            context.Set<Discount>().AddRange(items);
            context.SaveChanges();
        }

        private DiscountLimitationType GetRandomLimitType()
        {
            var x = new Random().Next(9000);
            if (x > 1000)
                return DiscountLimitationType.NTimesOnly;
            if (x > 7000)
                return DiscountLimitationType.NTimesPerCustomer;
            if (x > 5000)
                return DiscountLimitationType.Unlimited;
          
            return DiscountLimitationType.Unlimited;
        }

        private DiscountType GetRandomDiscountType()
        {
            var x = new Random().Next(9000);
            if (x > 1000)
                return DiscountType.AssignedToCategories;
            if (x > 2000)
                return DiscountType.AssignedToOrderSubTotal;
           
            if (x > 4000)
                return DiscountType.AssignedToSkus;
            if (x > 5000)
                return DiscountType.AssignedToShipping;

            return DiscountType.AssignedToShipping;
        }
    }
}
