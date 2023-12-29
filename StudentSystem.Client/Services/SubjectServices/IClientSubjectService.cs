using StudentSystem.Shared.DTOs;
using StudentSystem.Shared.Models;

namespace StudentSystem.Client.Services.SubjectServices
{
    public interface IClientSubjectService
    {
        List<Subject> subjects { get; set; }
        Task<List<Subject>> GetAllSubjects();
        Task<Subject> GetSingleSubject(int id);
        Task AddSubject(SubjectDTO subject);
        Task UpdateSubject(int id, SubjectDTO subject);
        Task<List<Subject>> DeleteSubject(int id);
    }
}
