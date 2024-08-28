using SimpleE_commerceAPI.Application.Common.Models;
using SimpleE_commerceAPI.Domain.Entities;

namespace SimpleE_commerceAPI.Application.Services.Interfaces
{
    public interface IApplicationUserService
    {
        IEnumerable<ApplicationUser> GetAllUsers();
        ApplicationUser GetUserById(string userId);
        ApplicationUser GetUserByEmail(string email);
        Task<IList<string>> GetUserRolesAsync(string email);
        bool CreateUser(RegisterModel model);
        bool UpdateUser(UpdateUserModel model);
        bool DeleteUser(string userId);
    }
}
