using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.DomainClasses.Entities.EShop;
using VarinaCmsV2.DomainClasses.Entities.EShop.Discounts;

namespace VarinaCmsV2.Services.Tests.MockDataServices
{
    public class MockDiscountDataService :MockBaseDataService<Discount>,IDiscountDataService
    {
       
        public MockDiscountDataService()
        {
            Items.AddRange(new List<Discount>());
        }
        public void ClearItems()
        {
           Items.Clear();
        }
    }
}
