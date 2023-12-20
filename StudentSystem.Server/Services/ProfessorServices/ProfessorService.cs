using Microsoft.EntityFrameworkCore;
using StudentSystem.Server.Data;
using StudentSystem.Shared.DTOs;
using StudentSystem.Shared.Models;

namespace StudentSystem.Server.Services.ProfessorServices
{
    public class ProfessorService : IProfessorService
    {
        private readonly DataContext _context;
        public ProfessorService(DataContext context)
        {
            _context = context;
        }

        

        public async Task<List<Professor>> GetAllProfessors()
        {
            var professors = await _context.Professors.ToListAsync();
            return professors;
        }

        public async Task<Professor> GetSingleProfessor(int id)
        {
            var professors = await _context.Professors
                     .Where(p => p.Id == id)
                     .FirstOrDefaultAsync();
            if (professors == null)
                return null;

            return professors;
        }
    }
}
