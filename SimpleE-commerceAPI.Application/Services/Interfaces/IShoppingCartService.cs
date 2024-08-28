using SimpleE_commerceAPI.Application.Common.Models;
using SimpleE_commerceAPI.Domain.Entities;

namespace SimpleE_commerceAPI.Application.Services.Interfaces
{
    public interface IShoppingCartService
    {
        Task<ShoppingCart> GetCartAsync(string userId);
        Task AddItemToCartAsync(string userId, CreateCartItemModel model);
        Task UpdateItemInCartAsync(string userId, string cartItemId, UpdateCartItemModel model);
        Task RemoveItemFromCartAsync(string userId, int cartItemId);
    }
}
