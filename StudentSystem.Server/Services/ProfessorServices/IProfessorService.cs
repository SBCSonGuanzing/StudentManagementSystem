using StudentSystem.Shared.DTOs;

namespace StudentSystem.Server.Services.ProfessorServices
{
    public interface IProfessorService
    {
        Task<List<Professor>> GetAllProfessors();
        Task<Professor> GetSingleProfessor(int id);

    }
}
