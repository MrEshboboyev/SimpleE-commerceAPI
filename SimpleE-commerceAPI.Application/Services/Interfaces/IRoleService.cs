using Microsoft.AspNetCore.Identity;
using SimpleE_commerceAPI.Application.Common.Models;

namespace SimpleE_commerceAPI.Application.Services.Interfaces
{
    public interface IRoleService
    {
        IEnumerable<IdentityRole> GetAllRoles();
        IdentityRole GetRoleById(string roleId);
        IdentityRole GetRoleByName(string roleName);
        Task<IList<string>> GetRoleUsersAsync(string roleName);
        bool CreateRole(CreateRoleModel model);
        bool UpdateRole(UpdateRoleModel model);
        bool DeleteRole(string roleId);
    }
}
