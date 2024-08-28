using SimpleE_commerceAPI.Application.Common.Models;
using SimpleE_commerceAPI.Domain.Entities;

namespace SimpleE_commerceAPI.Application.Services.Interfaces
{
    public interface IShoppingCartService
    {
        Task<ShoppingCart> GetCartAsync(string userId);
        Task<ShoppingCartItem> AddItemToCartAsync(string userId, CreateCartItemModel model);
        Task<ShoppingCartItem> UpdateItemInCartAsync(string userId, UpdateCartItemModel model);
        Task<bool> RemoveItemFromCartAsync(string userId, int cartItemId);
    }
}
