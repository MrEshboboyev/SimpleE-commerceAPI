using Microsoft.AspNetCore.Identity;

namespace SimpleE_commerceAPI.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string? FullName { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ShoppingCart ShoppingCart { get; set; }
    }
}
