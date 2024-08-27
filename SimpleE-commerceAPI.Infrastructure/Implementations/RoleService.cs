using Microsoft.AspNetCore.Identity;
using SimpleE_commerceAPI.Application.Common.Interfaces;
using SimpleE_commerceAPI.Application.Services.Interfaces;

namespace SimpleE_commerceAPI.Infrastructure.Implementations
{
    public class RoleService : IRoleService
    {
        // inject IUnitOfWork
        private readonly IUnitOfWork _unitOfWork;

        public RoleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void CreateRole(IdentityRole identityRole)
        {
            _unitOfWork.Role.Add(identityRole);
            _unitOfWork.Save();
        }

        public bool DeleteRole(string roleId)
        {
            try
            {
                IdentityRole? objFromDb = _unitOfWork.Role.Get(r => r.Id == roleId);
                if (objFromDb != null)
                {
                    _unitOfWork.Role.Remove(objFromDb);
                    _unitOfWork.Save();
                }
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
            return _unitOfWork.Role.Get(r => r.Name == roleName);
        }

        public void UpdateRole(IdentityRole identityRole)
        {
            _unitOfWork.Role.Update(identityRole);
            _unitOfWork.Save();
        }
    }
}
