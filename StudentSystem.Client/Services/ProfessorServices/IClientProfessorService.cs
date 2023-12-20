using StudentSystem.Shared.Models;

namespace StudentSystem.Client.Services.ProfessorServices
{
    public interface IClientProfessorService
    {
        List<Professor> professors { get; set; }

        Task<List<Professor>> GetAllProfessors();

    }
}
