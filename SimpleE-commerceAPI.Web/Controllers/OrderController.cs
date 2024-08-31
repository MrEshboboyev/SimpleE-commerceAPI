using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleE_commerceAPI.Application.Common.Models;
using SimpleE_commerceAPI.Application.Services.Interfaces;
using System.Security.Claims;

namespace SimpleE_commerceAPI.Web.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        // inject Payment, Order service
        private readonly IPaymentService _paymentService;
        private readonly IOrderService _orderService;

        public OrderController(IPaymentService paymentService,
            IOrderService orderService)
        {
            _paymentService = paymentService;
            _orderService = orderService;
        }

        [HttpPost("create-token")]
        public async Task<IActionResult> CreateToken([FromBody] CreateTokenModel model)
        {
            try
            {
                var token = _paymentService.CreatePaymentTokenAsync(model);
                return Ok(new { YourToken = token });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("create-order")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var orderRequestModel = new OrderRequestModel
                {
                    PaymentMethod = model.PaymentMethod,
                    PaymentToken = model.PaymentToken,
                    ShippingAddress = model.ShippingAddress,
                    UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                };
                var order = await _orderService.CreateOrderAsync(orderRequestModel);
                return Ok(order);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Order creation failed: {ex.Message}" });
            }
        }
    }
}
