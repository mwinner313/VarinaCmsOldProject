using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.Data.Contracts;
using VarinaCmsV2.DomainClasses.Entities.EShop;

namespace VarinaCmsV2.Services.DataServices
{
    public class ShoppingCartItemDataService : BaseDataService<ShoppingCartItem>, IShoppingCartItemDataService
    {
        public ShoppingCartItemDataService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
