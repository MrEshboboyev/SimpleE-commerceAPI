using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleE_commerceAPI.Application.Common.Models;
using SimpleE_commerceAPI.Application.Services.Interfaces;

namespace SimpleE_commerceAPI.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        // inject Product Service
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("get-all-products")]
        public async Task<IActionResult> GetAllProducts()
        {
            return Ok(_productService.GetAllProducts());
        }

        [HttpGet("get-product-by-id")]
        public async Task<IActionResult> GetProductById(int productId)
        {
            var result = _productService.GetProductById(productId);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost("create-product")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductModel model)
        {
            var result = _productService.CreateProduct(model);
            if (result == null)
            {
                return BadRequest("Create Product : Failed!");
            }

            return Ok(result);
        }

        [HttpPut("update-product")]
        public async Task<IActionResult> UpdateProduct(int productId, [FromBody] UpdateProductModel model)
        {
            if(productId != model.ProductId)
            {
                return BadRequest("productId and model.ProductId must be equal");
            }

            var result = _productService.UpdateProduct(model);
            if (result == null)
            {
                return BadRequest("Update Product : Failed!");
            }

            return Ok(result);
        }

        [HttpDelete("delete-product")]
        public async Task<IActionResult> RemoveProduct(int productId)
        {
            var isDeleted = _productService.DeleteProduct(productId);
            if (!isDeleted)
                return BadRequest("Delete Product : Failed!");

            return Ok("Delete Product : Success!");
        }
    }
}
