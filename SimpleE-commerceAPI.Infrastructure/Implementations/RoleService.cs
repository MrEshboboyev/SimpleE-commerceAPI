using Microsoft.AspNetCore.Identity;
using SimpleE_commerceAPI.Application.Common.Interfaces;
using SimpleE_commerceAPI.Application.Common.Models;
using SimpleE_commerceAPI.Application.Services.Interfaces;
using SimpleE_commerceAPI.Domain.Entities;
using System.Security.Cryptography.Xml;

namespace SimpleE_commerceAPI.Infrastructure.Implementations
{
    public class RoleService : IRoleService
    {
        // inject IUnitOfWork
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public RoleService(IUnitOfWork unitOfWork,
            UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public bool CreateRole(CreateRoleModel model)
        {
            try
            {
                if (_unitOfWork.Role.Any(r => r.Name == model.RoleName))
                {
                    return false;
                }
                var identityRole = new IdentityRole
                {
                    Name = model.RoleName,
                    NormalizedName = model.RoleName.ToUpper(),
                };
                _unitOfWork.Role.Add(identityRole);
                _unitOfWork.Save();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteRole(string roleId)
        {
            try
            {
                IdentityRole? objFromDb = _unitOfWork.Role.Get(r => r.Id == roleId);
                _unitOfWork.Role.Remove(objFromDb);
                _unitOfWork.Save();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<IdentityRole> GetAllRoles()
        {
            return _unitOfWork.Role.GetAll();
        }

        public IdentityRole GetRoleById(string roleId)
        {
            return _unitOfWork.Role.Get(r => r.Id == roleId);
        }

        public IdentityRole GetRoleByName(string roleName)
        {
            return _unitOfWork.Role.Get(r => r.Name.ToLower() == roleName.ToLower());
        }

        public bool UpdateRole(UpdateRoleModel model)
        {
            try
            {
                if (_unitOfWork.Role.Any(r => r.Name == model.RoleName))
                {
                    return false;
                }
                var identityRoleFromDb = _unitOfWork.Role.Get(r => r.Id == model.RoleId);

                // update fields 
                identityRoleFromDb.Name = model.RoleName;
                _unitOfWork.Role.Update(identityRoleFromDb);
                _unitOfWork.Save();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IList<string>> GetRoleUsersAsync(string roleName)
        {
            List<string> emails = new();
            try
            {
                var users = _unitOfWork.ApplicationUser.GetAll();
                foreach (var user in users)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    if (roles.Any() && roles.First().ToLower() == roleName.ToLower())
                    {
                        emails.Add(user.Email);
                    }
                }

                return emails;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
