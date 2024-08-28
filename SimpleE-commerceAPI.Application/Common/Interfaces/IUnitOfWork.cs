namespace SimpleE_commerceAPI.Application.Common.Interfaces
{
    public interface IUnitOfWork
    {
        IRoleRepository Role { get; }
        IApplicationUserRepository ApplicationUser { get; }
        IProductRepository Product { get; }
        IShoppingCartRepository Cart { get; }
        IShoppingCartItemRepository CartItem { get; }
        void Save();
    }
}
