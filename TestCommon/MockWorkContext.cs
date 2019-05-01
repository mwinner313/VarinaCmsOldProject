using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.Core.Contracts;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.Core.CustomAttributes;
using VarinaCmsV2.Core.Services.Product;
using VarinaCmsV2.DomainClasses.Entities;
using VarinaCmsV2.DomainClasses.Entities.EShop;
using VarinaCmsV2.DomainClasses.Users;

namespace TestCommon
{
    public class MockWorkContext : IWorkContext
    {
        private readonly IProductDataService _productDataService;

        public MockWorkContext(IProductDataService productDataService)
        {
            _productDataService = productDataService;
        }

        public Guid SelectedPaymentMethod { get; set; }=Guid.NewGuid();
        public Guid UserId { get; } = Guid.NewGuid();
        public Guid SelectedAddressId { get; set; } = Guid.NewGuid();
        public Guid StateProviceId { get; set; } = Guid.NewGuid();
        public User CurrentUser => new User()
        {
            Id = UserId
            ,
            ShoppingCartItems = GetProductFromMockDataService().ToList(),
            Attributes = new List<UserAttribute>()
            {
                new UserAttribute()
                {
                    Name = UserAttributes.PaymentProccessingOrderId,
                    Value =Guid.NewGuid().ToString(),
                    UserId = UserId,
                    Id = Guid.NewGuid(),
                },
                new UserAttribute()
                {
                    Name = UserAttributes.SelectedShippingAddressId,
                    Value = SelectedAddressId.ToString(),
                    UserId = UserId,
                    Id = Guid.NewGuid(),
                },
                new UserAttribute()
                {
                    Name = UserAttributes.SelectedPaymentMethod,
                    Value = SelectedPaymentMethod.ToString(),
                    UserId = UserId,
                    Id = Guid.NewGuid(),
                }
            },
            Addresses = new List<Address>()
            {
                new Address()
                {
                    Id = SelectedAddressId,
                    PhoneNumber = "09196644336",
                    Email = "m.ghanbari01375@gmail.com",
                    UserId = UserId,Path = "قم  خیابان",
                    ReciverName = "محمد علی",
                    StateProvinceId = StateProviceId,
                    StateProvince = new StateProvince()
                    {
                        Id = StateProviceId,
                        IsVisible = true,
                        Name = "قم"
                    }
                }
            }

        };

        private IEnumerable<ShoppingCartItem> GetProductFromMockDataService()
        {
            foreach (var product in _productDataService.Query.ToList())
            {
                yield return new ShoppingCartItem()
                {
                    Quantity = product.Inventory.StockQuantity - 3,
                    Product = product,
                    CreateDateTime = DateTime.Now,
                    ProductId = product.Id
                };
            }
        }
    }
}
