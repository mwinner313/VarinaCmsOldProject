using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VarinaCmsV2.DomainClasses.Entities.EShop.Discounts
{
    public enum DiscountLimitationType
    {
      
        Unlimited = 5,
        NTimesOnly = 15,
        NTimesPerCustomer = 25,
    }
}
