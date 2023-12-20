using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentSystem.Server.Services.ProfessorServices;

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
            return await _professorService.GetAllProfessors();
        }

    }
}
