using Microsoft.Extensions.Configuration;
using SimpleE_commerceAPI.Application.Common.Models;
using SimpleE_commerceAPI.Application.Services.Interfaces;
using Stripe;

namespace SimpleE_commerceAPI.Infrastructure.Implementations
{
    public class StripePaymentService : IPaymentService
    {
        public async Task<PaymentResult> ProcessPaymentAsync(PaymentRequest paymentRequest)
        {
            var options = new ChargeCreateOptions
            {
                Amount = (long)(paymentRequest.Amount * 100), // Amount in cents
                Currency = "usd",
                Description = paymentRequest.Description,
                Source = paymentRequest.Token
            };

            var service = new ChargeService();
            Charge charge = await service.CreateAsync(options);

            return new PaymentResult
            {
                Success = (charge.Status == "succeeded"),
                TransactionId = charge.Id,
                ErrorMessage = (charge.Status != "succeeded" ? charge.FailureMessage : null)
            };
        }
    }
}
