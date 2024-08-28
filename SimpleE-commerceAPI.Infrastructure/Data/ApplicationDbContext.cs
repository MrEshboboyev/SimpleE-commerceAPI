using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SimpleE_commerceAPI.Domain.Entities;
using System.Reflection.Emit;

namespace SimpleE_commerceAPI.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User to ShoppingCart (One-to-One)
            modelBuilder.Entity<ApplicationUser>()
                .HasOne(u => u.ShoppingCart)
                .WithOne(c => c.User)
                .HasForeignKey<ShoppingCart>(c => c.UserId);

            // User to Orders (One-to-Many)
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.Orders)
                .WithOne(o => o.User)
                .HasForeignKey(o => o.UserId);

            // ShoppingCart to ShoppingCartItems (One-to-Many)
            modelBuilder.Entity<ShoppingCart>()
                .HasMany(c => c.ShoppingCartItems)
                .WithOne(i => i.ShoppingCart)
                .HasForeignKey(i => i.ShoppingCartId);

            // Product to ShoppingCartItems (One-to-Many)
            modelBuilder.Entity<Product>()
                .HasMany(p => p.ShoppingCartItems)
                .WithOne(i => i.Product)
                .HasForeignKey(i => i.ProductId);

            // Order to OrderItems (One-to-Many)
            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderItems)
                .WithOne(i => i.Order)
                .HasForeignKey(i => i.OrderId);

            // Product to OrderItems (One-to-Many)
            modelBuilder.Entity<Product>()
                .HasMany(p => p.OrderItems)
                .WithOne(i => i.Product)
                .HasForeignKey(i => i.ProductId);

            // Order to Payment (One-to-One)
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Payment)
                .WithOne(p => p.Order)
                .HasForeignKey<Payment>(p => p.OrderId);
        }
    }
}
