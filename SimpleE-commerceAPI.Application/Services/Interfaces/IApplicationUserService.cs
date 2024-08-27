using SimpleE_commerceAPI.Domain.Entities;

namespace SimpleE_commerceAPI.Application.Services.Interfaces
{
    public interface IApplicationUserService
    {
        IEnumerable<ApplicationUser> GetAllUsers();
        ApplicationUser GetUserById(string userId);
        ApplicationUser GetUserByEmail(string email);
        void CreateUser(ApplicationUser applicationUser);
        void UpdateUser(ApplicationUser applicationUser);
        bool DeleteUser(string userId);
    }
}
