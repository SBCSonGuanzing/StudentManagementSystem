using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentSystem.Server.Services.UserServices;

namespace StudentSystem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public UserController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [HttpGet("single-avatar")]
        public async Task<ActionResult<string>> GetSingleUserAvater()
        {
            var result = await _userService.GetSingleUser();

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            var result = await _userService.DeleteUser(id);
            if (result is null)
            {
                return NotFound("User not found");
            }

            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> GetAllUsers()
        {
            var result = await _userService.GetAllUsers();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<int>> GetSingleStudent(int id)
        {
            var result = await _userService.GetSingleStudent(id);
            //if (result is null)
            //    return NotFound("Student not found.");
            return Ok(result);
        }

        [HttpGet("user-role")]
        public async Task<ActionResult<string>> GetUserRole()
        {
            var result = await _userService.GetUserRole();

            return Ok(result);
        }
    }
}
