using SimpleE_commerceAPI.Application.Common.Interfaces;
using SimpleE_commerceAPI.Domain.Entities;
using SimpleE_commerceAPI.Infrastructure.Data;

namespace SimpleE_commerceAPI.Infrastructure.Repositories
{
    public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
    {
        private readonly ApplicationDbContext _db;
        public ShoppingCartRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(ShoppingCart cart)
        {
            _db.ShoppingCarts.Update(cart);
        }
    }
}
