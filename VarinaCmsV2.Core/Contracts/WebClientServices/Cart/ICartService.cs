using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VarinaCmsV2.Core.Contracts.WebClientServices.Products;

namespace VarinaCmsV2.Core.Contracts.WebClientServices.Cart
{
    public interface ICartService
    {
        Task<AddToCartResponse> AddToCartAsync(Guid productId, int quantity);
        Task<RomveFromCartResponse> RemoveFromCartAsync(Guid productId);
        Task UpdateShoppingCartItems(List<ShoppingCartItemUpdateModel> updatedCartItems);
    }
}
