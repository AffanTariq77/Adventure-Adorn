using Blaze.Model.ViewModels;
using Blaze.Service.Contract;
using Microsoft.AspNetCore.Mvc;

namespace Blaze.Api.Controller
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserByIdAsync(Guid id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound(new { Message = "User not found." });
            }
            return Ok(user);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserAsync(Guid id, [FromBody] UserView userView)
        {
            if (userView == null || userView.Id != id)
            {
                return BadRequest(new { Message = "Invalid user data or ID mismatch." });
            }

            await _userService.UpdateUserAsync(userView);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserAsync(Guid id)
        {
            await _userService.DeleteUserAsync(id);
            return NoContent();
        }

        [HttpGet("heartbeat")]
        public IActionResult GetHeartbeat()
        {
            return Ok(new { Message = "API is running." });
        }
    }
}
