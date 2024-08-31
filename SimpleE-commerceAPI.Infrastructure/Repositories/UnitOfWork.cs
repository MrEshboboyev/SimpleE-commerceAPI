using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using SimpleE_commerceAPI.Application.Common.Interfaces;
using SimpleE_commerceAPI.Infrastructure.Data;

namespace SimpleE_commerceAPI.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        // inject Db Context, IDbContextTransaction
        private readonly ApplicationDbContext _db;
        private IDbContextTransaction _transaction;

        public IRoleRepository Role {  get; private set; }
        public IApplicationUserRepository ApplicationUser {  get; private set; }
        public IProductRepository Product {  get; private set; }
        public IShoppingCartRepository Cart { get; private set; }
        public IShoppingCartItemRepository CartItem { get; private set; }
        public IPaymentRepository Payment { get; private set; }
        public IOrderRepository Order { get; private set; }


        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Role = new RoleRepository(db);
            ApplicationUser = new ApplicationUserRepository(db);
            Product = new ProductRepository(db);
            Cart = new ShoppingCartRepository(db);
            CartItem = new ShoppingCartItemRepository(db);
            Payment = new PaymentRepository(db);
            Order = new OrderRepository(db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _db.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            try
            {
                await _db.SaveChangesAsync();
                await _transaction.CommitAsync();
            }
            catch
            {
                await RollbackTransactionAsync();
                throw;
            }
        }

        public async Task RollbackTransactionAsync()
        {
            await _transaction.RollbackAsync();
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _db.Dispose();
        }
    }
}
