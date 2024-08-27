using Microsoft.AspNetCore.Identity;

namespace SimpleE_commerceAPI.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string? FullName { get; set; }
    }
}
