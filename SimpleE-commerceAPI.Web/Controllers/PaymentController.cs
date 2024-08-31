using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleE_commerceAPI.Application.Common.Models;
using SimpleE_commerceAPI.Application.Services.Interfaces;

namespace SimpleE_commerceAPI.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        // inject Payment Service
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet("get-all-payments")]
        public async Task<IActionResult> GetAllPayments()
        {
            return Ok(await _paymentService.GetAllPaymentsAsync());
        }

        [HttpGet("get-payments-by-user")]
        public async Task<IActionResult> GetUserPayments(string userId)
        {
            return Ok(await _paymentService.GetUserPaymentsAsync(userId));
        }
        
        [HttpGet("get-payments-by-method")]
        public async Task<IActionResult> GetPaymentsByMethod(string paymentMethod)
        {
            return Ok(await _paymentService.GetPaymentsByMethodAsync(paymentMethod));
        }

        [HttpGet("get-payment-by-id")]
        public async Task<IActionResult> GetPaymentById(int paymentId)
        {
            var result = await _paymentService.GetPaymentByIdAsync(paymentId);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost("create-payment")]
        public async Task<IActionResult> CreatePayment([FromBody] CreatePaymentModel model)
        {
            var result = await _paymentService.CreatePaymentAsync(model);
            if (result == null)
            {
                return BadRequest("Create Payment : Failed!");
            }

            return Ok(result);
        }

        [HttpPut("update-payment")]
        public async Task<IActionResult> UpdatePayment(int paymentId, [FromBody] UpdatePaymentModel model)
        {
            if (paymentId != model.PaymentId)
            {
                return BadRequest("PaymentId and model.PaymentId must be equal");
            }

            var result = await _paymentService.UpdatePaymentAsync(model);
            if (result == null)
            {
                return BadRequest("Update Payment : Failed!");
            }

            return Ok(result);
        }

        [HttpDelete("delete-payment")]
        public async Task<IActionResult> RemovePayment(int paymentId)
        {
            var isDeleted = await _paymentService.RemovePaymentAsync(paymentId);
            if (!isDeleted)
                return BadRequest("Delete Payment : Failed!");

            return Ok("Delete Payment : Success!");
        }
    }
}
