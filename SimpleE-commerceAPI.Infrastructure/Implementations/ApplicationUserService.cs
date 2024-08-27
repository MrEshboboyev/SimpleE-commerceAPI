using SimpleE_commerceAPI.Application.Common.Interfaces;
using SimpleE_commerceAPI.Application.Services.Interfaces;
using SimpleE_commerceAPI.Domain.Entities;

namespace SimpleE_commerceAPI.Infrastructure.Implementations
{
    public class ApplicationUserService : IApplicationUserService
    {
        // inject IUnitOfWork
        private readonly IUnitOfWork _unitOfWork;

        public ApplicationUserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void CreateUser(ApplicationUser applicationUser)
        {
            _unitOfWork.ApplicationUser.Add(applicationUser);
            _unitOfWork.Save();
        }

        public bool DeleteUser(string userId)
        {
            try
            {
                ApplicationUser? objFromDb = _unitOfWork.ApplicationUser.Get(r => r.Id == userId);
                if (objFromDb != null)
                {
                    _unitOfWork.ApplicationUser.Remove(objFromDb);
                    _unitOfWork.Save();
                }
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

        public void UpdateUser(ApplicationUser applicationUser)
        {
            _unitOfWork.ApplicationUser.Update(applicationUser);
            _unitOfWork.Save();
        }
    }
}
