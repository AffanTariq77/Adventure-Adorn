using Blaze.Domain.Entities;
using Blaze.Model.ViewModels;
using Blaze.Repository.Contract;
using Blaze.Service.Contract;

namespace Blaze.Service.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWorkRepository _unitOfWorkRepository;

        public UserService(IUnitOfWorkRepository unitOfWorkRepository)
        {
            _unitOfWorkRepository = unitOfWorkRepository;
        }

        public async Task<User> GetUserByIdAsync(Guid id)
        {
            return await _unitOfWorkRepository.UserRepository.FindAsync(id);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _unitOfWorkRepository.UserRepository.GetAllAsync();
        }

        public async Task UpdateUserAsync(UserView userView)
        {
            if (userView == null)
                throw new ArgumentNullException(nameof(userView));

            var existingUser = await _unitOfWorkRepository.UserRepository.FindAsync(userView.Id);
            if (existingUser == null)
                throw new KeyNotFoundException("User not found.");

            existingUser.FirstName = userView.FirstName;
            existingUser.LastName = userView.LastName;
            existingUser.Email = userView.Email;
            existingUser.LastModifiedBy = userView.LastModifiedBy;
            existingUser.LastModifiedDate = DateTime.UtcNow;

            await _unitOfWorkRepository.UserRepository.UpdateAsync(existingUser);
            await _unitOfWorkRepository.CommitAsync();
        }

        public async Task DeleteUserAsync(Guid id)
        {
            var user = await _unitOfWorkRepository.UserRepository.FindAsync(id);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found.");
            }

            await _unitOfWorkRepository.UserRepository.RemoveAsync(id);
            await _unitOfWorkRepository.CommitAsync();
        }
    }
}
