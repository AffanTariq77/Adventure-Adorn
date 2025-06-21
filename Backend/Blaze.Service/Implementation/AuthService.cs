using Blaze.Common.Models;
using Blaze.Domain.Entities;
using Blaze.Model.Models.Auth;
using Blaze.Service.Contract;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Blaze.Service.Implementation
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly JwtSettings _jwtSettings;

        public AuthService(
            UserManager<User> userManager,
            RoleManager<IdentityRole<Guid>> roleManager,
            IOptions<JwtSettings> jwtSettings)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<AuthResponse> LoginAsync(LoginModel request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                return new AuthResponse
                {
                    IsSuccess = false,
                    Message = "Invalid email or password"
                };
            }

            var result = await _userManager.CheckPasswordAsync(user, request.Password);

            if (!result)
            {
                return new AuthResponse
                {
                    IsSuccess = false,
                    Message = "Invalid email or password"
                };
            }

            if (!user.IsActive)
            {
                return new AuthResponse
                {
                    IsSuccess = false,
                    Message = "User account is inactive"
                };
            }

            if (user.IsDeleted)
            {
                return new AuthResponse
                {
                    IsSuccess = false,
                    Message = "User account has been deleted"
                };
            }

            var userRoles = await _userManager.GetRolesAsync(user);

            return new AuthResponse
            {
                IsSuccess = true,
                Message = "Login successful",
                Token = await GenerateJwtToken(user, userRoles),
                Expiration = DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                UserName = user.UserName,
                Email = user.Email,
                Roles = userRoles.ToList()
            };
        }

        public async Task<AuthResponse> RegisterAsync(RegisterModel request)
        {
            var userExists = await _userManager.FindByEmailAsync(request.Email);

            if (userExists != null)
            {
                return new AuthResponse
                {
                    IsSuccess = false,
                    Message = "User with this email already exists"
                };
            }

            var user = new User
            {
                UserName = request.Email,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                MiddleName = request.MiddleName,
                CreatedDate = DateTime.UtcNow,
                IsActive = true,
                EmailConfirmed = true,  // For simplicity; in production, implement email confirmation
                CreatedBy = "system"
            };
            user.ModifiedBy("system");

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                return new AuthResponse
                {
                    IsSuccess = false,
                    Message = result.Errors.FirstOrDefault()?.Description ?? "User creation failed"
                };
            }

            // Assign default "User" role
            if (!await _roleManager.RoleExistsAsync("User"))
            {
                await _roleManager.CreateAsync(new IdentityRole<Guid>("User"));
            }

            await _userManager.AddToRoleAsync(user, "User");

            var roles = await _userManager.GetRolesAsync(user);

            return new AuthResponse
            {
                IsSuccess = true,
                Message = "User registered successfully",
                Token = await GenerateJwtToken(user, roles),
                Expiration = DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                UserName = user.UserName,
                Email = user.Email,
                Roles = roles.ToList()
            };
        }

        public async Task<AuthResponse> CreateUserAsync(RegisterModel request, string role)
        {
            var registerResult = await RegisterAsync(request);

            if (!registerResult.IsSuccess)
            {
                return registerResult;
            }

            var user = await _userManager.FindByEmailAsync(request.Email);

            if (!string.IsNullOrEmpty(role))
            {
                // Create role if it doesn't exist
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new IdentityRole<Guid>(role));
                }

                // Add role to user
                await _userManager.AddToRoleAsync(user, role);
            }

            var updatedRoles = await _userManager.GetRolesAsync(user);

            registerResult.Roles = updatedRoles.ToList();
            return registerResult;
        }

        public async Task<bool> AssignRoleAsync(string email, string role)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return false;
            }

            if (!await _roleManager.RoleExistsAsync(role))
            {
                await _roleManager.CreateAsync(new IdentityRole<Guid>(role));
            }

            await _userManager.AddToRoleAsync(user, role);
            return true;
        }

        private async Task<string> GenerateJwtToken(User user, IList<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("fullName", user.FullName),
                new Claim("userId", user.Id.ToString())
            };

            // Add role claims
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }
    }
}