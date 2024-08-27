using Microsoft.AspNetCore.Identity;

namespace SimpleE_commerceAPI.Application.Services.Interfaces
{
    public interface IRoleService
    {
        IEnumerable<IdentityRole> GetAllRoles();
        IdentityRole GetRoleById(string roleId);
        IdentityRole GetRoleByName(string roleName);
        void CreateRole(IdentityRole identityRole);
        void UpdateRole(IdentityRole identityRole);
        void DeleteRole(string roleId);
    }
}
