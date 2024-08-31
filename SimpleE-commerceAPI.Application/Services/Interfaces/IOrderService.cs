using SimpleE_commerceAPI.Application.Common.Models;
using SimpleE_commerceAPI.Domain.Entities;

namespace SimpleE_commerceAPI.Application.Services.Interfaces
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(OrderRequestModel model);
    }
}
