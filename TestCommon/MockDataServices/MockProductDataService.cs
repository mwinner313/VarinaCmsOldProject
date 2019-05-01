using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.DomainClasses.Entities.EShop;

namespace TestCommon.MockDataServices
{
    public class MockProductDataService:MockBaseDataService<Product>,IProductDataService
    {
        public MockProductDataService()
        {
           Items.AddRange(new List<Product>()
           {
               new Product()
               {
                   Id = Guid.NewGuid(),
                   Price = 890000,
                   Title = "لباس",Shipment = new ProductShipment()
                   {
                       IsShipEnabled = true
                   },
                   Inventory = new Inventory()
                   {
                       StockQuantity = 500
                   }
               },
               new Product()
               {
                   Id = Guid.NewGuid(),
                   Price = 570000,
                   Title = "شلوار",Shipment = new ProductShipment()
                   {
                       IsShipEnabled = true
                   },
                   Inventory = new Inventory()
                   {
                       StockQuantity = 500
                   }
               },
               new Product()
               {
                   Id = Guid.NewGuid(),
                   Price = 240000,
                   Title = "دست کش",Shipment = new ProductShipment()
                   {
                       IsShipEnabled = true
                   },
                   Inventory = new Inventory()
                   {
                       StockQuantity = 500
                   }
               },
               new Product()
               {
                   Id = Guid.NewGuid(),
                   Price = 6000000,
                   Title = "کلاه",Shipment = new ProductShipment()
                   {
                       IsShipEnabled = true
                   },
                   Inventory = new Inventory()
                   {
                       StockQuantity = 500
                   }
               },
               new Product()
               {
                   Id = Guid.NewGuid(),
                   Price = 230000,
                   Title = "دوچرخه",
                   Shipment = new ProductShipment()
                   {
                       IsShipEnabled = true
                   },
                   Inventory = new Inventory()
                   {
                       StockQuantity = 500
                   }
               },
               new Product()
               {
                   Id = Guid.NewGuid(),
                   Price = 8750000,
                   Title = "لبتاب",Shipment = new ProductShipment()
                   {
                       IsShipEnabled = true
                   },
                   Inventory = new Inventory()
                   {
                       StockQuantity = 500
                   }
               },
           });
        }
    }
}
