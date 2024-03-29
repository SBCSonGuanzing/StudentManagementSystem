﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentSystem.Server.Services.UserServices;
using System.Security.Claims;

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

        [HttpGet("single-student")]
        public async Task<ActionResult<Student>?> GetSingleStudent()
        {
            var result = await _userService.GetSingleStudent();
            if (result is null)
                return NotFound();
            return Ok(result);
        }

        [HttpGet("single-professor")]
        public async Task<ActionResult<Professor>?> GetSingleProfessor()
        {
            var result = await _userService.GetSingleStudent();
            if (result is null)
                return NotFound();
            return Ok(result);
        }

        [HttpGet("user-role")]
        public async Task<ActionResult<string>> GetUserRole()
        {
            var result = await _userService.GetUserRole();

            return Ok(result);
        }

        [HttpGet("user-id")]
        public async Task<ActionResult<string>> GetUserId()
        {
            var result = await _userService.GetUserId();

            return Ok(result);
        }

        [HttpGet("user-email")]
        public async Task<ActionResult<string>> GetUserEmail()
        {
            var result = await _userService.GetUserEmail();

            return Ok(result);
        }

        //[HttpGet("get-token")]
        //public async Task<ActionResult<string>> GetToken()
        //{
        //    var result = await _userService.GetToken();
        //    return Ok(result);
        //}

    }
}
