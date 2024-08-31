using SimpleE_commerceAPI.Application.Common.Models;

namespace SimpleE_commerceAPI.Application.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<PaymentResult> ProcessPaymentAsync(PaymentRequest paymentRequest);
    }
}
