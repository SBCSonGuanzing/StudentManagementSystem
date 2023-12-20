using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentSystem.Server.Services.BookServices;
using StudentSystem.Server.Services.SubjectServices;
using StudentSystem.Shared.DTOs;

namespace StudentSystem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectService _subjectService;

        public SubjectController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        [HttpPost]
        public async Task<ActionResult<Subject>> AddSubject(SubjectDTO subject)
        {
            var result = await _subjectService.AddSubject(subject);
            return Ok(result);
        }

        [HttpGet]

        public async Task<ActionResult<List<Subject>>> GetAllSubjects()
        {
            var result = await _subjectService.GetAllSubjects();
            return Ok(result);
        }

        [HttpDelete("delete-subject/{id}")]
        public async Task<ActionResult<Subject>> DeleteSubject(int id)
        {
            var result = await _subjectService.DeleteSubject(id);
            if (result is null)
            {
                return NotFound("Subject not found");
            }

            return Ok(result);
        }

        [HttpPut("update-subject/{id}")]
        public async Task<ActionResult<List<Subject>>> UpdateSubject(int id, SubjectDTO request)
        {
            var result = await _subjectService.UpdateSubject(id, request);
            if (result is null)
                return NotFound("Subject not found.");
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Subject>> GetSingleSubject(int id)
        {
            var result = await _subjectService.GetSingleSubject(id);
            if (result is null)
                return NotFound(" Subject not found.");
            return Ok(result);
        }
    }
}
