using Microsoft.AspNetCore.Identity;
using SimpleE_commerceAPI.Application.Common.Interfaces;
using SimpleE_commerceAPI.Infrastructure.Data;

namespace SimpleE_commerceAPI.Infrastructure.Repositories
{
    public class RoleRepository : Repository<IdentityRole>, IRoleRepository
    {
        private readonly ApplicationDbContext _db;

        public RoleRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(IdentityRole identityRole)
        {
            _db.Roles.Update(identityRole);
        }
    }
}
