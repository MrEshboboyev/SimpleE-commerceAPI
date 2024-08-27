using SimpleE_commerceAPI.Application.Common.Models;
using SimpleE_commerceAPI.Domain.Entities;

namespace SimpleE_commerceAPI.Application.Services.Interfaces
{
    public interface IAuthService
    {
        Task<string> LoginAsync(LoginModel loginModel);
        Task<string> RegisterAsync(RegisterModel registerModel);
        Task<string> GenerateJwtToken(ApplicationUser user, IEnumerable<string> roles);
    }
}
