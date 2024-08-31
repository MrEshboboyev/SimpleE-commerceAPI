using SimpleE_commerceAPI.Domain.Entities;

namespace SimpleE_commerceAPI.Application.Common.Interfaces
{
    public interface IPaymentRepository : IRepository<Payment>
    {
        void Update(Payment payment);
    }
}
