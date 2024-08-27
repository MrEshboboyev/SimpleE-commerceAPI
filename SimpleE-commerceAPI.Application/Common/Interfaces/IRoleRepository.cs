using Microsoft.AspNetCore.Identity;

namespace SimpleE_commerceAPI.Application.Common.Interfaces
{
    public interface IRoleRepository : IRepository<IdentityRole>
    {
        void Update(IdentityRole identityRole);
    }
}
