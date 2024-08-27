using Microsoft.AspNetCore.Identity;
using SimpleE_commerceAPI.Application.Common.Interfaces;
using SimpleE_commerceAPI.Domain.Entities;
using SimpleE_commerceAPI.Infrastructure.Data;

namespace SimpleE_commerceAPI.Infrastructure.Repositories
{
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        private readonly ApplicationDbContext _db;

        public ApplicationUserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(ApplicationUser applicationUser)
        {
            _db.Users.Update(applicationUser);
        }
    }
}
