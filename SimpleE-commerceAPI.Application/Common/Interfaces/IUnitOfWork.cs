namespace SimpleE_commerceAPI.Application.Common.Interfaces
{
    public interface IUnitOfWork  : IDisposable
    {
        IRoleRepository Role { get; }
        IApplicationUserRepository ApplicationUser { get; }
        IProductRepository Product { get; }
        IShoppingCartRepository Cart { get; }
        IShoppingCartItemRepository CartItem { get; }
        void Save();

        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
        Task SaveAsync();
    }
}
