using Blaze.Domain.Entities;
using Blaze.Model.ViewModels;

namespace Blaze.Service.Contract
{
    public interface IUserService
    {
        Task<User> GetUserByIdAsync(Guid id);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task UpdateUserAsync(UserView userView);
        Task DeleteUserAsync(Guid id);
    }
}
