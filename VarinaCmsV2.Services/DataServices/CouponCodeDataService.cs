using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.Data.Contracts;
using VarinaCmsV2.DomainClasses.Entities.EShop.Discounts;

namespace VarinaCmsV2.Services.DataServices
{
    public class CouponCodeDataService:BaseDataService<CouponCode>, ICouponCodeDataService
    {
        public CouponCodeDataService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
