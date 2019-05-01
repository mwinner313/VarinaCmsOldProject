using System;
using System.Collections.Generic;
using System.Linq;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.DomainClasses.Entities.EShop;
using VarinaCmsV2.DomainClasses.Entities.EShop.Discounts;

namespace VarinaCmsV2.Services.Tests.MockDataServices
{
    public class MockProductDataService : MockBaseDataService<Product>, IProductDataService
    {
        private readonly MockProductCategoryDataService _productCategoryDataService;
        public Guid ClotheId { get; set; }=Guid.NewGuid();
        public Guid LaptopId { get; set; }=Guid.NewGuid();
        public MockProductDataService(MockProductCategoryDataService productCategoryDataService)
        {
            _productCategoryDataService = productCategoryDataService;

            Items.AddRange(new List<Product>()
           {
               new Product()
               {
                   Id = ClotheId,
                   Price = 100,
                   Title = "لباس",Shipment = new ProductShipment()
                   {
                       IsShipEnabled = true
                   },
                   Inventory = new Inventory()
                   {
                       StockQuantity = 13
                   },
                   PrimaryCategory = _productCategoryDataService.Query.First(x=>x.Id==_productCategoryDataService.ClothesCatId)
                   ,
                   PrimaryCategoryId = _productCategoryDataService.ClothesCatId,
                   AppliedDiscounts =new List<Discount>()
               },
               new Product()
               {
                   Id = Guid.NewGuid(),
                   Price = 100,
                   Title = "شلوار",Shipment = new ProductShipment()
                   {
                       IsShipEnabled = true
                   },
                   Inventory = new Inventory()
                   {
                       StockQuantity = 13
                   },
                   PrimaryCategory = _productCategoryDataService.Query.First(x=>x.Id==_productCategoryDataService.ClothesCatId)
                   ,
                   PrimaryCategoryId = _productCategoryDataService.ClothesCatId,
                   AppliedDiscounts =new List<Discount>()
               },
               new Product()
               {
                   Id = Guid.NewGuid(),
                   Price = 100,
                   Title = "دست کش کریسمس",Shipment = new ProductShipment()
                   {
                       IsShipEnabled = true
                   },
                   Inventory = new Inventory()
                   {
                       StockQuantity = 13
                   },
                   PrimaryCategory = _productCategoryDataService.Query.First(x=>x.Id==_productCategoryDataService.ChirstMasGlovesCatId)
                   ,
                   PrimaryCategoryId = _productCategoryDataService.ClothesCatId,
                   AppliedDiscounts =new List<Discount>()
               },
               new Product()
               {
                   Id = Guid.NewGuid(),
                   Price = 100,
                   Title = "دست کش",Shipment = new ProductShipment()
                   {
                       IsShipEnabled = true
                   },
                   Inventory = new Inventory()
                   {
                       StockQuantity = 13
                   },
                   PrimaryCategory = _productCategoryDataService.Query.First(x=>x.Id==_productCategoryDataService.GlovesCatId)
                   ,
                   PrimaryCategoryId = _productCategoryDataService.ClothesCatId,
                   AppliedDiscounts =new List<Discount>()
               },
               new Product()
               {
                   Id = Guid.NewGuid(),
                   Price = 100,
                   Title = "کلاه",Shipment = new ProductShipment()
                   {
                       IsShipEnabled = true
                   },
                   Inventory = new Inventory()
                   {
                       StockQuantity = 13
                   },
                   PrimaryCategory = _productCategoryDataService.Query.First(x=>x.Id==_productCategoryDataService.ClothesCatId)
                   ,
                   PrimaryCategoryId = _productCategoryDataService.ClothesCatId,
                   AppliedDiscounts =new List<Discount>()
               },
               new Product()
               {
                   Id = Guid.NewGuid(),
                   Price = 100,
                   Title = "دوچرخه",
                   Shipment = new ProductShipment()
                   {
                       IsShipEnabled = true
                   },
                   Inventory = new Inventory()
                   {
                       StockQuantity = 13
                   },
                   PrimaryCategory = _productCategoryDataService.Query.First(x=>x.Id==_productCategoryDataService.KidsCatId),
                   PrimaryCategoryId = _productCategoryDataService.KidsCatId,
                   AppliedDiscounts =new List<Discount>()
               },
               new Product()
               {
                   Id = LaptopId,
                   Price = 500,
                   Title = "لبتاب",Shipment = new ProductShipment()
                   {
                       IsShipEnabled = true
                   },
                   Inventory = new Inventory()
                   {
                       StockQuantity = 13
                   },
                   PrimaryCategory = _productCategoryDataService.Query.First(x=>x.Id==_productCategoryDataService.ElectronicsCatId),
                   PrimaryCategoryId = _productCategoryDataService.ElectronicsCatId,
                   AppliedDiscounts =new List<Discount>()
               },
           });
        }
    }
}
