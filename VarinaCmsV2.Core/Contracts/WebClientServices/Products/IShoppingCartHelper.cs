using System.Threading.Tasks;
using VarinaCmsV2.Core.Contracts.Security;
using VarinaCmsV2.Data.Contracts;
using VarinaCmsV2.DomainClasses.Entities.EShop;
using VarinaCmsV2.DomainClasses.Users;

namespace VarinaCmsV2.Core.Contracts.WebClientServices.Products
{
    public interface IShoppingCartHelper
    {
        Task AddToCartAsync(Product product, int quantity, User user, IUnitOfWork unitOfWork);
        string GetAddToCartValidationErrors(User user, Product product, int quantity, IRestrictedItemAccessManager accessManager, IUnitOfWork unitOfWork);
        string GetRemoveFromCartValidationErrors(User user, Product product);
        void HandleUserCartWarnings(User user);
        Task RemoveFromCart(Product product, User user, IUnitOfWork unitOfWork);
    }
}