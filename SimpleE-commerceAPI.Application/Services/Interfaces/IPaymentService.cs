using SimpleE_commerceAPI.Application.Common.Models;
using SimpleE_commerceAPI.Domain.Entities;

namespace SimpleE_commerceAPI.Application.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<PaymentResult> ProcessPaymentAsync(PaymentRequest paymentRequest);
        Task<string> CreatePaymentTokenAsync(CreateTokenModel model);

        // only admins
        Task<IEnumerable<Payment>> GetAllPaymentsAsync();
        Task<IEnumerable<Payment>> GetUserPaymentsAsync(string userId);
        Task<IEnumerable<Payment>> GetPaymentByMethodAsync(string paymentMethod);
        Task<Payment> GetPaymentByIdAsync(int paymentId);
        Task<bool> CreatePaymentAsync(CreatePaymentModel model);
        Task<bool> UpdatePaymentAsync(UpdatePaymentModel model);
        Task<bool> RemovePaymentAsync(int paymentId);
    }
}
