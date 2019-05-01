using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.DomainClasses.Entities.EShop.Orders;

namespace VarinaCmsV2.Services.Tests.MockDataServices
{
    public class MockOrderDataService:MockBaseDataService<Order>,IOrderDataService
    {
    }
}
