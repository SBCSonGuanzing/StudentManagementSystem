using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentSystem.Server.Services.BookServices;
using StudentSystem.Server.Services.EnrolledSubjectsServices;
using StudentSystem.Shared.DTOs;

namespace StudentSystem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrolledSubjectsController : ControllerBase
    {
        private readonly IEnrolledSubjectsService _enrolledSubjectsService;

        public EnrolledSubjectsController(IEnrolledSubjectsService enrolledSubjectsService)
        {
            _enrolledSubjectsService = enrolledSubjectsService;
        }

        [HttpPost("add-enrolled-sub")]
        public async Task<ActionResult<int>> AddEnrolledSubjects(EnrollmentDTO request)
        {
            var result = await _enrolledSubjectsService.AddEnrolledSubjects(request);
            if(result == 0)
            {
                return BadRequest(result);
            } 
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<List<EnrolledSubjects>>> GetAllEnrolledSubject()
        {

            return await _enrolledSubjectsService.GetAllEnrolledSubject();
        }

        // Get a Single Enrolled Subject 
        [HttpGet("{id}")]
        public async Task<ActionResult<EnrolledSubjects>> GetSingleEnrolledSubject(int id)
        {
            var result = await _enrolledSubjectsService.GetSingleEnrolledSubjects(id);
            if (result is null)
                return NotFound("Enrolled Subject not found.");
            return Ok(result);

        }
        
    }
}
