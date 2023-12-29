using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentSystem.Server.Services.ProfessorServices;
using StudentSystem.Shared.DTOs;

namespace StudentSystem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessorController : ControllerBase
    {
        private readonly IProfessorService _professorService;

        public ProfessorController(IProfessorService professorService)
        {
            _professorService = professorService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Professor>>> GetAllProfessors()
        {
            return await _professorService.GetProfessors();
        }

        [HttpPut("update-professor/{id}")]
        public async Task<ActionResult<List<Professor>>> UpdateStudent(int id, UserDetailsDTO request)
        {
            var result = await _professorService.UpdateProfessor(id, request);
            if (result is null)
                return NotFound("Professor not found.");
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Professor>> GetSingleProfessor(int id)
        {
            var result = await _professorService.GetSingleProfessor(id);
            if (result is null)
                return NotFound("Professor not found.");
            return Ok(result);
        }

    }
}
