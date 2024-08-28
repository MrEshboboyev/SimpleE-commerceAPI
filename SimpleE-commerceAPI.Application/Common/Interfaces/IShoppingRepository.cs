using SimpleE_commerceAPI.Domain.Entities;

namespace SimpleE_commerceAPI.Application.Common.Interfaces
{
    public interface IShoppingRepository : IRepository<ShoppingCart>
    {
        void Update(ShoppingCart cart);
    }
}
