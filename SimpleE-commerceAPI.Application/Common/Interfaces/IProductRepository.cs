using SimpleE_commerceAPI.Domain.Entities;

namespace SimpleE_commerceAPI.Application.Common.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        void Update(Product product);
    }
}
