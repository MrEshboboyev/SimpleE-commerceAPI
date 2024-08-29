using Microsoft.AspNetCore.Identity;
using SimpleE_commerceAPI.Application.Common.Interfaces;
using SimpleE_commerceAPI.Application.Common.Models;
using SimpleE_commerceAPI.Application.Services.Interfaces;
using SimpleE_commerceAPI.Domain.Entities;

namespace SimpleE_commerceAPI.Infrastructure.Implementations
{
    public class ApplicationUserService : IApplicationUserService
    {
        // inject IUnitOfWork
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ApplicationUserService(IUnitOfWork unitOfWork,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<bool> CreateUserAsync(RegisterModel model)
        {
            try
            {
                if (_unitOfWork.ApplicationUser.Any(u => u.Email == model.Email))
                {
                    return false;
                }
                var applicationUser = new ApplicationUser
                {
                    Email = model.Email,
                    FullName = model.FullName,
                    NormalizedEmail = model.Email.ToUpper(),
                    UserName = model.Email
                };
                if (_roleManager.RoleExistsAsync(model.RoleName).GetAwaiter().GetResult())
                {
                    await _userManager.CreateAsync(applicationUser, model.Password);
                    await _userManager.AddToRoleAsync(applicationUser, model.RoleName);
                }
                _unitOfWork.Save();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteUser(string userId)
        {
            try
            {
                ApplicationUser? objFromDb = _unitOfWork.ApplicationUser.Get(r => r.Id == userId);
                _unitOfWork.ApplicationUser.Remove(objFromDb);
                _unitOfWork.Save();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<ApplicationUser> GetAllUsers()
        {
            return _unitOfWork.ApplicationUser.GetAll();
        }

        public ApplicationUser GetUserById(string userId)
        {
            return _unitOfWork.ApplicationUser.Get(r => r.Id == userId);
        }

        public ApplicationUser GetUserByEmail(string email)
        {
            return _unitOfWork.ApplicationUser.Get(r => r.Email == email);
        }

        public bool UpdateUser(UpdateUserModel model)
        {
            try
            {
                var user = _unitOfWork.ApplicationUser.Get(u => u.Id == model.UserId);
                user.FullName = model.FullName;
                user.Email = model.Email;
                if (!_roleManager.RoleExistsAsync(model.RoleName).GetAwaiter().GetResult())
                {
                    _userManager.AddToRoleAsync(user, model.RoleName);
                }
                _unitOfWork.ApplicationUser.Update(user);
                _unitOfWork.Save();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IList<string>> GetUserRolesAsync(string email)
        {
            try
            {
                var user = _unitOfWork.ApplicationUser.Get(u => u.Email == email);
                return await _userManager.GetRolesAsync(user);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
