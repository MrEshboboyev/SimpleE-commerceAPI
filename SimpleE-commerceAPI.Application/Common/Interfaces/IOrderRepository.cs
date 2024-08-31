using SimpleE_commerceAPI.Domain.Entities;

namespace SimpleE_commerceAPI.Application.Common.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        void Update(Order order);
    }
}
