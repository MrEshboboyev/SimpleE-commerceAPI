using SimpleE_commerceAPI.Application.Common.Interfaces;
using SimpleE_commerceAPI.Domain.Entities;
using SimpleE_commerceAPI.Infrastructure.Data;

namespace SimpleE_commerceAPI.Infrastructure.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Product product)
        {
            _db.Products.Update(product);
        }
    }
}
