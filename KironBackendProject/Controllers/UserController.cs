using KironBackendProject.Data.Dtos;
using KironBackendProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KironBackendProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest createUserRequest)
        {
            var result = await _userService.CreateUserAsync(createUserRequest);
            if (result != null)
            {
                return Ok(result);

            }
            return BadRequest("User already exists");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthRequest loginRequest)
        {
            var result = await _userService.LoginAsync(loginRequest);
            if (result == null)
                return Unauthorized("Invalid username or password");

            return Ok(result);
        }
    }
}
