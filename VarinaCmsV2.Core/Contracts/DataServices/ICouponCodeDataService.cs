﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.DomainClasses.Entities.EShop.Discounts;

namespace VarinaCmsV2.Core.Contracts.DataServices
{
    public interface ICouponCodeDataService:IDataService<CouponCode,Guid>
    {
    }
}
