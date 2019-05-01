using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.DomainClasses.Entities.EShop.Orders;

namespace VarinaCmsV2.Core.Contracts.DataServices
{
    public interface IOrderDataService:IDataService<Order,Guid>
    {
    }
}
