
using StudentSystem.Server.Data;
using StudentSystem.Shared.DTOs;

namespace StudentSystem.Server.Services.ProfessorServices
{
    public class ProfessorService : IProfessorService
    {
        private readonly DataContext _context;

        public ProfessorService(DataContext context)
        {
            _context = context;
        }
       

        public async Task<List<Professor>> GetProfessors()
        {
            var prof = await _context.Professors.ToListAsync();
            return prof;
        }

        public async Task<Professor> GetSingleProfessor(int id)
        {
            var prof = await _context.Professors
                    .Where(p => p.Id == id)
                    .FirstOrDefaultAsync();
            if (prof == null)
                return null;

            return prof;
        }

        public async Task<List<Professor>> UpdateProfessor(int id, UserDetailsDTO request)
        {
            var prof = await _context.Professors
                .Where(prof => prof.Id == id)
                .FirstOrDefaultAsync();

            if (prof == null)
                return null;

            prof.FirstName = request.FirstName;
            prof.LastName = request.LastName;
            prof.BirthDate = request.BirthDate;
            prof.Contact = request.Contact;
            prof.Image = request.Image;
            prof.Address = request.Address;

            await _context.SaveChangesAsync();

            return await GetProfessors();
        }
    }
}
