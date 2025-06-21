using Blaze.Model.Models.Auth;

namespace Blaze.Service.Contract
{
    public interface IAuthService
    {
        Task<AuthResponse> LoginAsync(LoginModel request);
        Task<AuthResponse> RegisterAsync(RegisterModel request);
        Task<AuthResponse> CreateUserAsync(RegisterModel request, string role);
        Task<bool> AssignRoleAsync(string email, string role);
    }

}
