using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleE_commerceAPI.Application.Common.Models;
using SimpleE_commerceAPI.Application.Services.Interfaces;
using System.Security.Claims;

namespace SimpleE_commerceAPI.Web.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        // inject Shopping Cart Service
        private readonly IShoppingCartService _cartService;

        public ShoppingCartController(IShoppingCartService cartService)
        {
            _cartService = cartService;
        }

        #region Private Methods
        private string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
        #endregion

        [HttpGet("get-cart")]
        public async Task<IActionResult> GetCartAsync()
        {
            return Ok(await _cartService.GetCartAsync(GetUserId()));
        }

        [HttpPost("add-item-to-cart")]
        public async Task<IActionResult> AddItemToCartAsync([FromBody] CreateCartItemModel model)
        {
            var result = await _cartService.AddItemToCartAsync(GetUserId(), model);
            if (result is null)
                return BadRequest("Create Cart Item : Failed!");

            return Ok(result);
        }

        [HttpPut("update-item-in-cart")]
        public async Task<IActionResult> UpdateItemInCartAsync(int cartItemId, [FromBody] UpdateCartItemModel model)
        {
            if (cartItemId != model.ShoppingCartItemId)
                return BadRequest("CartItemId and model.ShoppingCartItemId must be equal!");

            var result = await _cartService.UpdateItemInCartAsync(GetUserId(), model);
            if (result is null)
                return BadRequest("Update Cart Item : Failed!");

            return Ok(result);
        }

        [HttpDelete("delete-item-from-cart")]
        public async Task<IActionResult> DeleteItemInCartAsync(int cartItemId)
        {
            var result = await _cartService.RemoveItemFromCartAsync(GetUserId(), cartItemId);
            if (!result)
                return BadRequest("Delete Cart Item : Failed!");

            return Ok("Delete Cart Item : Success!");
        }
    }
}
