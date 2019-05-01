using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.DomainClasses.Entities.EShop;
using VarinaCmsV2.ViewModel.Eshop;

namespace VarinaCmsV2.Core.LogicFacade
{
    public interface IProductFacade
    {
        Task AddAsync(Product product);
        Task DeleteAsync(Product model);
        Task UpdateAsync(Product product, ProductAddOrUpdateModel model);
    }
}
