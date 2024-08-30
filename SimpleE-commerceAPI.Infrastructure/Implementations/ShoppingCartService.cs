using SimpleE_commerceAPI.Application.Common.Interfaces;
using SimpleE_commerceAPI.Application.Common.Models;
using SimpleE_commerceAPI.Application.Services.Interfaces;
using SimpleE_commerceAPI.Domain.Entities;

namespace SimpleE_commerceAPI.Infrastructure.Implementations
{
    public class ShoppingCartService : IShoppingCartService
    {
        // inject IUnitOfWork
        private readonly IUnitOfWork _unitOfWork;

        public ShoppingCartService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region Private Methods
        private decimal CalculatePrice(int quantity, int productId)
        {
            return quantity * _unitOfWork.Product.Get(p => p.ProductId == productId).Price;
        }
        
        private bool CalculateTotalAmount(int shoppingCartId)
        {
            try
            {
                var cartFromDb = _unitOfWork.Cart.Get(c => c.ShoppingCartId == shoppingCartId);
                cartFromDb.TotalAmount = _unitOfWork.CartItem.GetAll(item => item.ShoppingCartId == shoppingCartId).Sum(item => item.Price);
                _unitOfWork.Cart.Update(cartFromDb);
                _unitOfWork.Save();
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }
        #endregion

        public async Task<ShoppingCartItem> AddItemToCartAsync(string userId, CreateCartItemModel model)
        {
            try
            {
                // if user haven't cart, create it
                if (!_unitOfWork.Cart.Any(c => c.UserId == userId))
                {
                    // get cart by user Id
                    _unitOfWork.Cart.Add(new ShoppingCart { UserId = userId });
                }

                // getting cart by userId
                var cart = _unitOfWork.Cart.Get(c => c.UserId == userId);

                // check this item.product already exist in this cart
                var existingItem = _unitOfWork.CartItem.Get(item => 
                    item.ShoppingCartId == cart.ShoppingCartId && item.ProductId == model.ProductId);

                if (existingItem is not null)
                {
                    existingItem.Quantity += model.Quantity;
                    existingItem.Price = CalculatePrice(existingItem.Quantity, existingItem.ProductId);
                    _unitOfWork.CartItem.Update(existingItem);
                    _unitOfWork.Save();
                    return existingItem;
                }
                else
                {
                    var cartItem = new ShoppingCartItem
                    {
                        ShoppingCartId = cart.ShoppingCartId,
                        Quantity = model.Quantity,
                        Price = CalculatePrice(model.Quantity, model.ProductId),
                        ProductId = model.ProductId
                    };
                    _unitOfWork.CartItem.Add(cartItem);
                    _unitOfWork.Save();
                    return cartItem;
                }
            }
            catch
            {
                return null;
            }
        }

        public async Task<ShoppingCart> GetCartAsync(string userId)
        {
            if (!_unitOfWork.Cart.Any(c => c.UserId == userId))
            {
                _unitOfWork.Cart.Add(new ShoppingCart { UserId = userId });
                _unitOfWork.Save();
            }

            var cartFromDb = _unitOfWork.Cart.Get(c => c.UserId == userId);

            // prepare total amount and save
            var result = CalculateTotalAmount(cartFromDb.ShoppingCartId);
            if (!result)
                return null;

            return _unitOfWork.Cart.Get(c => c.UserId == userId, 
                includeProperties: "ShoppingCartItems");
        }

        public async Task<bool> RemoveItemFromCartAsync(string userId, int cartItemId)
        {
            try
            {
                // getting this Cart for this user
                var cartFromDb = _unitOfWork.Cart.Get(c => c.UserId == userId);

                // getting cart Item for this cartItemId and cart.ShoppingCartId
                var cartItemFromDb = _unitOfWork.CartItem.Get(item => 
                    item.ShoppingCartId == cartFromDb.ShoppingCartId && 
                    item.ShoppingCartItemId == cartItemId);

                _unitOfWork.CartItem.Remove(cartItemFromDb);
                _unitOfWork.Save();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<ShoppingCartItem> UpdateItemInCartAsync(string userId, UpdateCartItemModel model)
        {
            try
            {
                // getting this cart by userId
                var cart = _unitOfWork.Cart.Get(c => c.UserId == userId);

                // getting this cartItem by cart.ShoppingCartId, cartItemId
                var cartItemFromDb = _unitOfWork.CartItem.Get(item => 
                    item.ShoppingCartId == cart.ShoppingCartId && 
                    item.ShoppingCartItemId == model.ShoppingCartItemId);

                // update cartItem
                cartItemFromDb.Quantity = model.Quantity;
                cartItemFromDb.Price = CalculatePrice(model.Quantity, cartItemFromDb.ProductId);

                _unitOfWork.CartItem.Update(cartItemFromDb);
                _unitOfWork.Save();
                return cartItemFromDb;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
