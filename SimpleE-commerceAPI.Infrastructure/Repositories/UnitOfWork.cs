using SimpleE_commerceAPI.Application.Common.Interfaces;
using SimpleE_commerceAPI.Infrastructure.Data;

namespace SimpleE_commerceAPI.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        // inject Db Context
        private readonly ApplicationDbContext _db;
        public IRoleRepository Role {  get; private set; }
        public IApplicationUserRepository ApplicationUser {  get; private set; }
        public IProductRepository Product {  get; private set; }
        public IShoppingCartRepository Cart { get; private set; }


        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Role = new RoleRepository(db);
            ApplicationUser = new ApplicationUserRepository(db);
            Product = new ProductRepository(db);
            Cart = new ShoppingCartRepository(db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
