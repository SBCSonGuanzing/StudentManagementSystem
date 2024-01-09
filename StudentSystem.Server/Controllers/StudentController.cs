using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentSystem.Server.Services.StudentServices;
using StudentSystem.Shared.DTOs;

namespace StudentSystem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Student>>> GetAllStudents()
        {
            var result = await _studentService.GetAllStudents();
            return Ok(result);
        }

        [HttpPut("update-student/{id}")]
        public async Task<ActionResult<List<Student>>> UpdateStudent(int id, UserDetailsDTO request)
        {
            var result = await _studentService.UpdateStudent(id, request);
            if (result is null)
                return NotFound("Student not found.");
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetSingleStudent(int id)
        {
            var result = await _studentService.GetSingleStudent(id);
            if (result is null)
                return NotFound("Student not found.");
            return Ok(result);
        }

    }
}
