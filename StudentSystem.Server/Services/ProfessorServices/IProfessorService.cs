using StudentSystem.Shared.DTOs;

namespace StudentSystem.Server.Services.ProfessorServices
{
    public interface IProfessorService
    {
        Task<List<Professor>> UpdateProfessor(int id, UserDetailsDTO request);
        Task<Professor> GetSingleProfessor(int id);
        Task<List<Professor>> GetProfessors();

    }
}
