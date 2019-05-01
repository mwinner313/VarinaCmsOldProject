using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.DomainClasses.Entities.EShop.Discounts;
using VarinaCmsV2.ViewModel.Eshop.Discount;

namespace VarinaCmsV2.Core.LogicFacade
{
    public interface IDiscountFacade
    {
        Task AddAsync(Discount discount);
        Task UpadateAsync(Discount discount, DiscountAddOrUpdateModel updated);
    }
}
