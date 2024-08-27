using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SimpleE_commerceAPI.Application.Common.Utility;
using SimpleE_commerceAPI.Application.Services.Interfaces;
using SimpleE_commerceAPI.Domain.Entities;
using SimpleE_commerceAPI.Infrastructure.Data;

namespace SimpleE_commerceAPI.Infrastructure.Implementations
{
    public class DbInitializer : IDbInitializer
    {
        // inject Identity Managers and Db
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _db;

        public DbInitializer(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext db)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _db = db;
        }

        public void Initialize()
        {
            try
            {
                // migrate
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }

                // is Admin role is not exist, created roles (Customer and Admin)
                if (!_roleManager.RoleExistsAsync(SD.Role_Admin).GetAwaiter().GetResult())
                {
                    _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin));
                    _roleManager.CreateAsync(new IdentityRole(SD.Role_Customer));

                    // create admin user
                    _userManager.CreateAsync(new ApplicationUser
                    {
                        UserName = "admin@example.com",
                        Email = "admin@example.com",
                        FullName = "John Doe",
                        NormalizedUserName = "ADMIN@EXAMPLE.COM",
                        NormalizedEmail = "ADMIN@EXAMPLE.COM",
                        PhoneNumber = "1112223333",
                    }, "Admin*123").GetAwaiter().GetResult();

                    // finding user
                    ApplicationUser user = _db.Users.FirstOrDefault(
                        u => u.Email == "admin@example.com"
                        );

                    _userManager.AddToRoleAsync(user, SD.Role_Admin).GetAwaiter().GetResult();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
